package com.example.lab7

import android.app.ActionBar.LayoutParams
import android.graphics.Color
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ImageButton
import android.widget.LinearLayout
import androidx.fragment.app.Fragment
import com.android.volley.*
import com.android.volley.toolbox.JsonObjectRequest
import com.android.volley.toolbox.Volley
import com.google.android.material.textfield.TextInputEditText
import org.json.JSONObject


class LEDFragmentViewModel : Fragment() {

    var selectedColour: String = "#000000"
    lateinit var colorInputText: TextInputEditText
    lateinit var colorApplyButton: Button
    lateinit var sendRequestButton:Button

    var colorData: MutableList<String> = List(64) { "#000000" }.toMutableList()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_led, container, false)

        colorInputText = view.findViewById(R.id.colorInputText)
        colorApplyButton = view.findViewById(R.id.colorApplyButton)

        colorApplyButton.setOnClickListener {
            selectedColour = colorInputText.text.toString()
        }

        sendRequestButton = view.findViewById(R.id.sendRequestButton)

        sendRequestButton.setBackgroundColor(Color.GREEN)
        sendRequestButton.setOnClickListener {
            val volleyQueue = Volley.newRequestQueue(activity)
            val url = "https://httpbin.org/post" // TODO: Change for release

            var JSONData: JSONObject = JSONObject()

            JSONData.put("data", colorData)

            val jsonObjectRequest = JsonObjectRequest(
                Request.Method.POST,
                url,
                JSONData,
                {
                        response ->
                    Log.d("Response", response.toString())
                    sendRequestButton.setBackgroundColor(Color.GREEN)

                },
                {
                        error ->
                    print("Error Occured something wrong with http request")
                }
            )
            volleyQueue.add(jsonObjectRequest)
        }

        // Adding buttons to represent all of the LED of
        val verticalLinearLayout: LinearLayout = view.findViewById(R.id.verticalLinearLayout)
        verticalLinearLayout.weightSum = 8.0f
        for (y in 0..7) {

            var horizontalLinearLayout = LinearLayout(view.context)
            horizontalLinearLayout.orientation = LinearLayout.HORIZONTAL
            horizontalLinearLayout.layoutParams = LinearLayout.LayoutParams(LayoutParams.MATCH_PARENT,LayoutParams.WRAP_CONTENT, 1.0F )
            horizontalLinearLayout.weightSum = 8.0f
            for (x in 0..7) {

                var currentLedButton = ImageButton(horizontalLinearLayout.context)
                val buttonLayoutParams = LayoutParams(70, 70 )
                currentLedButton.layoutParams = buttonLayoutParams
                currentLedButton.setBackgroundColor(Color.parseColor("#f0f0f0")) // grey colour

                currentLedButton.setOnClickListener {
                    currentLedButton.setBackgroundColor(Color.parseColor(selectedColour))
                    colorData[y*8+x] = selectedColour
                    sendRequestButton.setBackgroundColor(Color.RED)
                }

                horizontalLinearLayout.addView(currentLedButton)

                var dummyView = View(horizontalLinearLayout.context)
                dummyView.layoutParams = LayoutParams(5, 5 )
                horizontalLinearLayout.addView(dummyView)
            }
            verticalLinearLayout.addView(horizontalLinearLayout)
        }

        return view
    }

}