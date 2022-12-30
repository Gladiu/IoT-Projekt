package com.example.lab7

import com.example.lab7.LEDModel
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


class LEDViewModel : Fragment() {
    val LEDModelObject:LEDModel = LEDModel()
    lateinit var colorInputText: TextInputEditText
    lateinit var colorApplyButton: Button
    lateinit var sendRequestButton:Button

    var buttonArray = mutableListOf<ImageButton>()


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_led, container, false)

        LEDModelObject.initSettings(this.requireActivity())

        colorInputText = view.findViewById(R.id.colorInputText)
        colorApplyButton = view.findViewById(R.id.colorApplyButton)

        colorApplyButton.setOnClickListener {
            LEDModelObject.selectedColour = colorInputText.text.toString()
        }

        sendRequestButton = view.findViewById(R.id.sendRequestButton)

        sendRequestButton.setBackgroundColor(Color.GREEN)
        sendRequestButton.setOnClickListener {
            LEDModelObject.sendLEDPOSTRequest(this.requireActivity())
            sendRequestButton.setBackgroundColor(Color.GREEN)
        }
        sendRequestButton.isActivated = false
        colorApplyButton.isActivated = false

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


                currentLedButton.setBackgroundColor(Color.parseColor( LEDModelObject.getColorAt(x,y) ))

                currentLedButton.setOnClickListener {
                    try {
                        currentLedButton.setBackgroundColor(Color.parseColor(LEDModelObject.selectedColour))
                    }catch (exc: Throwable){}
                    LEDModelObject.setColorAt(x,y)
                    LEDModelObject.recentlyChangedLeds.add(y*8+x)
                    sendRequestButton.setBackgroundColor(Color.RED)
                }
                buttonArray.add(currentLedButton)
                horizontalLinearLayout.addView(currentLedButton)

                var dummyView = View(horizontalLinearLayout.context)
                dummyView.layoutParams = LayoutParams(5, 5 )
                horizontalLinearLayout.addView(dummyView)
            }
            verticalLinearLayout.addView(horizontalLinearLayout)
        }

        LEDModelObject.sendLEDGETRequest(this.requireActivity(), buttonArray)

        return view
    }



}