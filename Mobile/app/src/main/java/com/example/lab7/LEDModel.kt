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

/**
 * @brief Data class for LED
 *
 * @description Class that encapsulates all the information about one LED
 *
 * @property selectedColour hex RGB value of currently selected colour
 * @property colorData List containing data of all LEDS
 * @property recentlyChangedLeds List containing data of recently changed LEDS
 * @property url used to GET and POST LED data
 * @property volleyQueue queue used to make GET requests
 * @constructor Initializes empty model. Fills LED with default colour
 */

class LEDModel {
    var selectedColour: String = "#f0f0f0"
    var colorData: MutableList<LEDData> = mutableListOf<LEDData>()
    var recentlyChangedLeds: MutableList<Int> = ArrayList(64)

    var url = ""

    lateinit var volleyQueue:RequestQueue

    /**
     * Initializes empty model. Initializes LED data with empty colour
     */
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

    /**
     * Initializes settings that need activity
     */
    fun initSettings(currentActivity: FragmentActivity){
        volleyQueue = Volley.newRequestQueue(currentActivity)
        url = ""
        try {
            url = currentActivity.getPreferences(MODE_PRIVATE).getString("IP","")!!
        }
        catch (exc: Throwable){} // we shouldnt ever get here, if we do its issue with settings
        url += "/post/Leds"
    }


    /**
     * Sends POST request to url containing newly changed LED data
     */
    fun sendLEDPOSTRequest(currentActivity:FragmentActivity){

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

    /**
     * Sends GET request to display actual colour of fragments
     */
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

    /**
     * Helper functions used to retrieve colour of specific LED using its x and y position
     * @return returns RGB hex value of colour
     */
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

    /**
     * Helper functions used to retrieve colour of specific LED using its index
     * @return returns RGB hex value of colour
     */
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


    /**
     * Helper functions used to set colour of specific LED
     */
    fun setColorAt(x:Int, y:Int){
        colorData[y*8+x].R = selectedColour.substring(1,3).toInt(16)
        colorData[y*8+x].G = selectedColour.substring(4,5).toInt(16)
        colorData[y*8+x].B = selectedColour.substring(6,7).toInt(16)
    }



}