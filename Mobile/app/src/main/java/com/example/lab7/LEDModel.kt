package com.example.lab7

import android.app.Activity
import android.content.Context.MODE_PRIVATE
import android.graphics.Color
import android.util.Log
import androidx.fragment.app.FragmentActivity
import com.android.volley.Request
import com.android.volley.toolbox.JsonObjectRequest
import com.android.volley.toolbox.Volley
import org.json.JSONObject

class LEDModel {
    var selectedColour: String = "#000000"
    var colorData: MutableList<String> = List(64) { "#000000" }.toMutableList()

    fun sendLEDRequest(currentActivity:FragmentActivity){
        val volleyQueue = Volley.newRequestQueue(currentActivity)
        val url = currentActivity.getPreferences(MODE_PRIVATE).getString("IP","")

        var JSONData: JSONObject = JSONObject()

        JSONData.put("data", colorData)

        val jsonObjectRequest = JsonObjectRequest(
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
        volleyQueue.add(jsonObjectRequest)
    }

    fun setColorAt(x:Int, y:Int){
        colorData[y*8+x] = selectedColour
    }
}