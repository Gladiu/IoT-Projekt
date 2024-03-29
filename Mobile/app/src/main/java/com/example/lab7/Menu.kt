package com.example.lab7

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import androidx.navigation.Navigation


/**
 * @brief Main Menu
 *
 * @description Main Menu of the application. It redirects to desired fragments
 */
class menu : Fragment() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_menu, container, false)
        val buttonPlot: Button = view.findViewById(R.id.buttonPlot)
        buttonPlot.setOnClickListener {
            Navigation.findNavController(view).navigate(R.id.action_menu_to_graph)
        }
        val buttonLED: Button = view.findViewById(R.id.buttonLED)
        buttonLED.setOnClickListener {
            Navigation.findNavController(view).navigate(R.id.action_menu_to_LEDFragment)
        }
        val buttonRawData: Button = view.findViewById(R.id.buttonRawData)
        buttonRawData.setOnClickListener {
            Navigation.findNavController(view).navigate(R.id.action_menu_to_rawData)
        }
        val buttonSettings: Button = view.findViewById(R.id.buttonSettings)
        buttonSettings.setOnClickListener {
            Navigation.findNavController(view).navigate(R.id.action_menu_to_settingsViewModel)
        }
        return view
    }

}