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
            .putString("IP", "https://b6bd4311-6494-495a-a73c-25ae508bb185.mock.pstmn.io")
            .commit()
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val navHostFragment =
            supportFragmentManager.findFragmentById(R.id.nav_host_fragment) as NavHostFragment
        val navController = navHostFragment.navController
    }
}