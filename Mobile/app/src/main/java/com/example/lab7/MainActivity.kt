package com.example.lab7

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.fragment.app.FragmentActivity
import androidx.navigation.fragment.NavHostFragment

/**
 * @brief Main activity of the class
 *
 * @description Used to set default settings
 */
class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        getPreferences(MODE_PRIVATE)
            .edit()
            .putLong("sampleTime", 100L)
            .commit()

        getPreferences(MODE_PRIVATE)
            .edit()
            .putString("IP", "http://192.168.1.5:5000")
            .commit()
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }
}