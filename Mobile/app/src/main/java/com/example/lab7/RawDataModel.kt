package com.example.lab7

import android.content.Context
import android.graphics.Color
import android.widget.LinearLayout
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentActivity
import androidx.lifecycle.lifecycleScope
import com.android.volley.Request
import com.android.volley.RequestQueue
import com.android.volley.toolbox.JsonArrayRequest
import com.android.volley.toolbox.Volley
import com.jjoe64.graphview.series.DataPoint
import com.jjoe64.graphview.series.LineGraphSeries
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.delay
import kotlinx.coroutines.isActive
import kotlinx.coroutines.launch

class RawDataModel {

    var currentTime = 1.0
    var lastTime = 0.0
    var cycleTime = 0L


    var temperature:Double = 0.0
    var pressure:Double = 0.0
    var humidity:Double = 0.0
    var roll:Double  = 0.0
    var pitch:Double = 0.0
    var yaw:Double = 0.0

    var url = ""

    lateinit var volleyQueue: RequestQueue


    fun initSettings(currentActivity: FragmentActivity) {

        volleyQueue = Volley.newRequestQueue(currentActivity)
        try {
            url = currentActivity.getPreferences(Context.MODE_PRIVATE).getString("IP","")!!
        }
        catch (exc: Throwable){} // we shouldnt ever get here, if we do its issue with settings
        url += "/get/DataObjects"

        cycleTime = currentActivity.getPreferences(Context.MODE_PRIVATE).getLong("sampleTime",0)

    }

    fun startTimer(currentFragment: Fragment,linearLayout: LinearLayout) {
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
                                    if (currentJSONOBject.getString("name") == "temperature") {
                                        temperature = currentJSONOBject.getDouble("value")
                                    }
                                    if (currentJSONOBject.getString("name") == "pressure") {
                                        pressure = currentJSONOBject.getDouble("value")
                                    }
                                    if (currentJSONOBject.getString("name") == "humidity") {
                                        humidity = currentJSONOBject.getDouble("value")
                                    }
                                    if (currentJSONOBject.getString("name") == "roll") {
                                        roll = currentJSONOBject.getDouble("value")
                                    }
                                    if (currentJSONOBject.getString("name") == "pitch") {
                                        pitch = currentJSONOBject.getDouble("value")
                                    }
                                    if (currentJSONOBject.getString("name") == "yaw") {
                                        yaw = currentJSONOBject.getDouble("value")
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