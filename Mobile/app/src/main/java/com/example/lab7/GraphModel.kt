package com.example.lab7

import android.content.Context
import android.graphics.Color
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentActivity
import com.android.volley.Request
import com.android.volley.toolbox.JsonArrayRequest
import com.android.volley.toolbox.Volley
import com.jjoe64.graphview.series.DataPoint
import com.jjoe64.graphview.series.LineGraphSeries
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.isActive
import kotlinx.coroutines.launch
import androidx.lifecycle.lifecycleScope
import com.android.volley.RequestQueue
import kotlinx.coroutines.*

class GraphModel {
    var currentTime = 1.0
    var lastTime = 0.0
    var cycleTime = 0L

    var url = ""

    var temperatureSeries: LineGraphSeries<DataPoint>
    var pressureSeries: LineGraphSeries<DataPoint>
    var humiditySeries: LineGraphSeries<DataPoint>

    lateinit var volleyQueue: RequestQueue

    init {

        temperatureSeries = LineGraphSeries(arrayOf(DataPoint(0.0, 0.0)))
        pressureSeries = LineGraphSeries(arrayOf(DataPoint(0.0, 0.0)))
        humiditySeries = LineGraphSeries(arrayOf(DataPoint(0.0, 0.0)))
        temperatureSeries.title = "Temperature [C]"
        pressureSeries.title = "Pressure [hPa]"
        humiditySeries.title = "Humidity [%]"

        temperatureSeries.color = Color.BLUE
        pressureSeries.color = Color.GREEN
        humiditySeries.color = Color.RED
    }


    fun initSettings(currentActivity: FragmentActivity) {

        volleyQueue = Volley.newRequestQueue(currentActivity)
        try {
            url = currentActivity.getPreferences(Context.MODE_PRIVATE).getString("IP","")!!
        }
        catch (exc: Throwable){} // we shouldnt ever get here, if we do its issue with settings
        url += "/get/DataObjects"

        cycleTime = currentActivity.getPreferences(Context.MODE_PRIVATE).getLong("sampleTime",0)

    }

    fun startTimer(currentFragment: Fragment) {
        val timerName = currentFragment.lifecycleScope.launch(Dispatchers.IO) {
            while (isActive) {
                currentFragment.lifecycleScope.launch {

                    val jsonObjectRequest = JsonArrayRequest(
                        Request.Method.GET,
                        url,
                        null,
                        { response ->
                            if (lastTime < currentTime) {

                                for (index in 0 until response.length()) {
                                    val currentJSONOBject = response.getJSONObject(index)
                                    if (currentJSONOBject.getString("name") == "temperature") {
                                        temperatureSeries.appendData(
                                            DataPoint(
                                                currentTime / 1000,
                                                response.getJSONObject(index).getDouble("value")
                                            ),
                                            false, 10
                                        )
                                    }
                                    if (currentJSONOBject.getString("name") == "pressure") {
                                        pressureSeries.appendData(
                                            DataPoint(
                                                currentTime / 1000,
                                                response.getJSONObject(index).getDouble("value")
                                            ),
                                            false, 10
                                        )
                                    }

                                    if (currentJSONOBject.getString("name") == "humidity") {
                                        humiditySeries.appendData(
                                            DataPoint(
                                                currentTime / 1000,
                                                response.getJSONObject(index).getDouble("value")
                                            ),
                                            false, 10
                                        )
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