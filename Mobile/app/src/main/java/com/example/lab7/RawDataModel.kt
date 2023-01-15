package com.example.lab7

import android.content.Context
import android.widget.EditText
import android.widget.LinearLayout
import android.widget.TextView
import androidx.core.view.children
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentActivity
import androidx.lifecycle.lifecycleScope
import com.android.volley.Request
import com.android.volley.RequestQueue
import com.android.volley.toolbox.JsonArrayRequest
import com.android.volley.toolbox.Volley
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.delay
import kotlinx.coroutines.isActive
import kotlinx.coroutines.launch
import java.text.DecimalFormat

/**
 * @brief Data class for RawData
 *
 * @description Class that encapsulates all the information from physical sensors
 *
 * @property currentTime current time
 * @property lastTime last recorded time
 * @property cycleTime cycle time for updating the graph
 * @property url url that provides data for graph
 * @property volleyQueue queue used to make GET requests
 * @property measurementsList lists that holds all data about measurements
 * @constructor Initializes empty model
 */

class RawDataModel {

    var currentTime = 1.0
    var lastTime = 0.0
    var cycleTime = 0L

    var url = ""

    lateinit var volleyQueue: RequestQueue

    val measurementsList: MutableList<RawDataData> = mutableListOf<RawDataData>()

    /**
     * Initializes settings that need activity
     */
    fun initSettings(currentActivity: FragmentActivity) {

        volleyQueue = Volley.newRequestQueue(currentActivity)
        try {
            url = currentActivity.getPreferences(Context.MODE_PRIVATE).getString("IP","")!!
        }
        catch (exc: Throwable){} // we shouldnt ever get here, if we do its issue with settings
        url += "/get/DataObjects"

        cycleTime = currentActivity.getPreferences(Context.MODE_PRIVATE).getLong("sampleTime",0)

    }

    /**
     * Starts updating data on a timer
     */
    fun startTimer(currentFragment: Fragment, linearLayout: LinearLayout) {
        val timerName = currentFragment.lifecycleScope.launch(Dispatchers.IO) {
            while (isActive) {
                currentFragment.lifecycleScope.launch {

                    val jsonObjectRequest = JsonArrayRequest(
                        Request.Method.GET,
                        url,
                        //"http://217.182.75.146/index.php", // TODO: CHANGE
                        null,
                        { response ->
                            if (lastTime < currentTime) {

                                for (index in 0 until response.length()) {
                                    val currentJSONOBject = response.getJSONObject(index)
                                    val currentRawData = measurementsList.find { it.name.contains(currentJSONOBject.getString("name"), ignoreCase = true)}
                                    if ( currentRawData!= null){
                                        var currentIndex = measurementsList.indexOf(currentRawData)
                                        var child = linearLayout.children.find { it is TextView && it.text.contains(currentJSONOBject.getString("name"), ignoreCase = true)}
                                        child = child as TextView
                                        var newValue = currentJSONOBject.getDouble("value").toDouble()
                                        when (measurementsList[currentIndex].unit) {
                                            "F" -> {
                                                newValue = newValue*1.8 + 32
                                            }
                                            "Pa" -> {newValue=newValue/100}
                                            "rad" -> {newValue = (newValue/360)*2*3.14}
                                            else -> {}
                                        }
                                        var newValueString = DecimalFormat("#.###").format(newValue)
                                        child.text = measurementsList[currentIndex].name + ": " + newValueString + " [" + measurementsList[currentIndex].unit + "]"
                                    }
                                    else{
                                        val newTextView = TextView(linearLayout.context)
                                        var newRawData = RawDataData( currentJSONOBject.getDouble("value"),
                                                                        currentJSONOBject.getString("name"),
                                                                        currentJSONOBject.getString("defaultUnit")
                                                                        )
                                        measurementsList.add(newRawData)
                                        newTextView.text =
                                            currentJSONOBject.getString("name") + ": " + currentJSONOBject.getDouble(
                                                "value"
                                            ).toString()
                                        newTextView.text = newRawData.name  + ": " + newRawData.value.toString() + " [" + newRawData.unit + "]"
                                        newTextView.textSize = 20F

                                        var currentIndex = measurementsList.indexOf(
                                                                            measurementsList.find {
                                                                                it.name.contains(currentJSONOBject.getString("name"),
                                                                                    ignoreCase = true)}
                                                                            )
                                        newTextView.setOnClickListener {
                                            changeUnits(currentIndex)
                                        }
                                        linearLayout.addView(newTextView)
                                    }
                                    lastTime = currentTime
                                }
                            }

                        },
                        { error ->
                            print("Error Occured something wrong with http request")
                        }
                    )
                    volleyQueue.add(jsonObjectRequest)

                }
                currentTime += cycleTime
                delay(cycleTime)
            }
        }
    }

    /**
     * Changes unit for measurement with selected index in measurementList
     */
    fun changeUnits(index:Int){
        var newUnit = ""
        when (measurementsList[index].unit) {
            "C" -> {
                newUnit = "F"
            }
            "F" -> {
                newUnit = "C"
            }
            "Pa" -> {
                newUnit = "hPa"
            }
            "hPa" -> {
                newUnit = "Pa"
            }
            "deg" -> {
                newUnit = "rad"
            }
            "rad" -> {
                newUnit = "deg"
            }
            else -> {
                newUnit = measurementsList[index].unit
            }
        }
        measurementsList[index].unit = newUnit
    }
}