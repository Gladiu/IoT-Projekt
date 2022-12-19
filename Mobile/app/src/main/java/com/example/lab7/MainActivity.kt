package com.example.lab7

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.fragment.app.FragmentActivity
import androidx.navigation.fragment.NavHostFragment

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        getPreferences(MODE_PRIVATE)
            .edit()
            .putLong("sampleTime", 100L)
            .commit()

        getPreferences(MODE_PRIVATE)
            .edit()
            .putString("IP", "http://217.182.75.146/index.php")
            .commit()
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val navHostFragment =
            supportFragmentManager.findFragmentById(R.id.nav_host_fragment) as NavHostFragment
        val navController = navHostFragment.navController
    }
}