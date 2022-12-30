package com.example.lab7

import android.content.Context.MODE_PRIVATE
import android.graphics.Color
import android.util.Log
import android.widget.Button
import android.widget.ImageButton
import androidx.fragment.app.FragmentActivity
import com.android.volley.Request
import com.android.volley.RequestQueue
import com.android.volley.toolbox.JsonArrayRequest
import com.android.volley.toolbox.Volley
import org.json.JSONArray
import org.json.JSONObject


class LEDModel {
    var selectedColour: String = "#f0f0f0"
    var colorData: MutableList<LEDData> = mutableListOf<LEDData>()
    var recentlyChangedLeds: MutableList<Int> = ArrayList(64)

    lateinit var volleyQueue:RequestQueue

    init{
        for (y in 0..7) {
            for (x in 0..7) {
                var currentLEDData = LEDData(x,y,
                                             selectedColour.substring(1,3).toInt(16),
                                             selectedColour.substring(4,5).toInt(16),
                                             selectedColour.substring(6,7).toInt(16)
                                            )

                colorData.add(currentLEDData)
            }
        }
    }

    fun initVolleyQueue(currentActivity: FragmentActivity){
        volleyQueue = Volley.newRequestQueue(currentActivity)
    }


    fun sendLEDPOSTRequest(currentActivity:FragmentActivity){
        var url = currentActivity.getPreferences(MODE_PRIVATE).getString("IP","")
        url += "/post/Leds"

        var JSONData: JSONArray = JSONArray()
        for (id in recentlyChangedLeds){
            var currentJsonObject = JSONObject()
            currentJsonObject.put("x", colorData[id].x)
            currentJsonObject.put("y", colorData[id].y)
            currentJsonObject.put("R", colorData[id].R)
            currentJsonObject.put("G", colorData[id].G)
            currentJsonObject.put("B", colorData[id].B)
            JSONData.put(currentJsonObject)
        }

        recentlyChangedLeds.clear()

        val jsonArrayRequest = JsonArrayRequest(
            Request.Method.POST,
            url,
            JSONData,
            {
                    response ->
                Log.d("Response", response.toString())

            },
            {
                    error ->
                print("Error Occured something wrong with http request")
            }
        )
        volleyQueue.add(jsonArrayRequest)
    }

    fun sendLEDGETRequest(currentActivity:FragmentActivity, buttonList: MutableList<ImageButton>){

        var url = currentActivity.getPreferences(MODE_PRIVATE).getString("IP","")
        url += "/get/Leds"

        val jsonArrayRequest = JsonArrayRequest(
            Request.Method.GET,
            url,
            null,
            {
                    response ->
                for (index in 0 until response.length()){

                    val currentX = response.getJSONObject(index).getInt("x")
                    val currentY = response.getJSONObject(index).getInt("y")
                    try {
                        val currentIndex = colorData.indexOf(colorData.find{ it.x == currentX && it.y == currentY }!!)
                        colorData[currentIndex].R = response.getJSONObject(index).getInt("R")
                        colorData[currentIndex].G = response.getJSONObject(index).getInt("G")
                        colorData[currentIndex].B = response.getJSONObject(index).getInt("B")
                    }
                    catch (exc: Throwable){} // we shouldnt ever get here, if we do its issue with server
                }

            },
            {
                    error ->
                print("Error Occured something wrong with http request")
            }
        )
        volleyQueue.add(jsonArrayRequest)
        volleyQueue.addRequestEventListener { request, event ->
            if (event == RequestQueue.RequestEvent.REQUEST_FINISHED){
                for ((index, button) in buttonList.withIndex()){
                    button.setBackgroundColor(Color.parseColor(this.getColor(index)))
                }
                recentlyChangedLeds.clear()
            }
        }
    }

    fun getColorAt(x:Int, y:Int) : String {
        var returnString = "#"

        val currentR = colorData[y * 8 + x].R.toString(16)
        if (currentR.length != 2){
            returnString += "0"
        }
        returnString += currentR
        val currentG = colorData[y * 8 + x].G.toString(16)
        if (currentG.length != 2){
            returnString += "0"
        }
        returnString += currentG
        val currentB = colorData[y * 8 + x].B.toString(16)
        if (currentB.length != 2){
            returnString += "0"
        }
        returnString += currentB
        return returnString
    }

    fun getColor(index: Int) : String {
        var returnString = "#"

        val currentR = colorData[index].R.toString(16)
        if (currentR.length != 2){
            returnString += "0"
        }
        returnString += currentR
        val currentG = colorData[index].G.toString(16)
        if (currentG.length != 2){
            returnString += "0"
        }
        returnString += currentG
        val currentB = colorData[index].B.toString(16)
        if (currentB.length != 2){
            returnString += "0"
        }
        returnString += currentB
        return returnString
    }


    fun setColorAt(x:Int, y:Int){
        colorData[y*8+x].R = selectedColour.substring(1,3).toInt(16)
        colorData[y*8+x].G = selectedColour.substring(4,5).toInt(16)
        colorData[y*8+x].B = selectedColour.substring(6,7).toInt(16)
    }



}