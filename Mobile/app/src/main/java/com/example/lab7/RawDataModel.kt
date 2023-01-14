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
 * @constructor Initializes empty model
 */

class RawDataModel {

    var currentTime = 1.0
    var lastTime = 0.0
    var cycleTime = 0L

    var url = ""

    lateinit var volleyQueue: RequestQueue


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
                        "http://217.182.75.146/index.php", // TODO: CHANGE
                        null,
                        { response ->
                            if (lastTime < currentTime) {

                                for (index in 0 until response.length()) {
                                    val currentJSONOBject = response.getJSONObject(index)
                                    val child = linearLayout.children.find { it is TextView && it.text.contains(currentJSONOBject.getString("name"), ignoreCase = true)}
                                    if (child != null && child is TextView){
                                        child.text = currentJSONOBject.getString("name") + ": " + currentJSONOBject.getDouble("value").toString()
                                    }
                                    else{
                                        val newTextView = TextView(linearLayout.context)
                                        newTextView.text =
                                            currentJSONOBject.getString("name") + ": " + currentJSONOBject.getDouble(
                                                "value"
                                            ).toString()
                                        newTextView.textSize = 20F
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
}