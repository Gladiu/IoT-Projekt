package com.example.lab7

import android.app.ActionBar.LayoutParams
import android.content.Context
import android.graphics.Color
import android.widget.LinearLayout
import android.widget.TextView
import androidx.core.view.children
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
import com.jjoe64.graphview.GraphView
import kotlinx.coroutines.*

/**
 * @brief Model of Graph classes
 *
 * @description This class is a model used in View creating graphs.
 *
 * @property currentTime current time
 * @property lastTime last recorded time
 * @property cycleTime cycle time for updating the graph
 * @property url url that provides data for graph
 * @property currentData index of currentlt displayed Series
 * @property measurementSeries series holding Series  data
 * @property volleyQueue queue used to make GET requests
 * @constructor Initializes empty model
 */
class GraphModel {
    var currentTime = 1.0
    var lastTime = 0.0

    var currentData = 0

    var cycleTime = 0L
    var url = ""

    var measurementSeries: MutableList<GraphData> = mutableListOf<GraphData>()

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
        url += "/get/DataStructs"

        cycleTime = currentActivity.getPreferences(Context.MODE_PRIVATE).getLong("sampleTime",0)

    }

    /**
     * Starts updating data graphs on a timer
     */
    fun startTimer(currentFragment: Fragment, graphView: GraphView) {
        val timerName = currentFragment.lifecycleScope.launch(Dispatchers.IO) {
            while (isActive) {
                currentFragment.lifecycleScope.launch {

                    val jsonObjectRequest = JsonArrayRequest(
                        Request.Method.GET,
                        //"http://217.182.75.146/index.php",
                        url,
                        null,
                        { response ->
                            if (lastTime < currentTime) {

                                for (index in 0 until response.length()) {
                                    val currentJSONOBject = response.getJSONObject(index)
                                    val child = measurementSeries.find { it.name.contains(currentJSONOBject.getString("name"), ignoreCase = true)}
                                    if (child != null){
                                        val currentIndex = measurementSeries.indexOf(child)
                                        var newValue =  response.getJSONObject(index).getDouble("value")
                                        when (measurementSeries[currentIndex].unit) {
                                            "F" -> {
                                                newValue = newValue*1.8 + 32
                                            }
                                            "Pa" -> {newValue=newValue/100}
                                            "rad" -> {newValue = (newValue/360)*2*3.14}
                                            else -> {}
                                        }
                                        measurementSeries[currentIndex].measurement.appendData(
                                            DataPoint(
                                                currentTime / 1000,
                                                newValue
                                            ),
                                            false, 10
                                        )
                                    }
                                    else{
                                        val newGraphData = GraphData(LineGraphSeries(arrayOf(DataPoint(0.0, 0.0))),
                                                                        currentJSONOBject.getString("name"),
                                                                        currentJSONOBject.getString("defaultUnit") )
                                        newGraphData.measurement.title = newGraphData.name + " ["+ newGraphData.unit + "]"
                                        newGraphData.measurement.color = Color.BLUE

                                        measurementSeries.add(newGraphData)
                                        graphView.removeAllSeries()
                                        graphView.addSeries(measurementSeries[0].measurement)

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
     * Increments index of currently displayed graph
     */
    fun cycleCurrentData(graphView: GraphView){
        currentData++
        if (currentData >= measurementSeries.size){
            currentData = 0
        }
        graphView.removeAllSeries()
        graphView.addSeries(measurementSeries[currentData].measurement)
    }

    /**
     * Changes units of currently displayed graph
     */
    fun changeUnits(graphView: GraphView){

        var newUnit = ""
        measurementSeries[currentData].measurement = LineGraphSeries(arrayOf(DataPoint(0.0, 0.0)))

        when (measurementSeries[currentData].unit) {
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
                newUnit = measurementSeries[currentData].unit
            }
        }
        measurementSeries[currentData].unit = newUnit
        measurementSeries[currentData].measurement.title = measurementSeries[currentData].name + " ["+ measurementSeries[currentData].unit + "]"
        graphView.removeAllSeries()
        graphView.addSeries(measurementSeries[currentData].measurement)

    }

}