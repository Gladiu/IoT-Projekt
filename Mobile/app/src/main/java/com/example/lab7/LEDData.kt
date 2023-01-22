package com.example.lab7

/**
 * @brief Data class for LED
 *
 * @description Class that encapsulates all the information about one LED
 *
 * @property x value between 0 and 7 representing x position of led
 * @property y value between 0 and 7 representing y position of led
 * @property R value between 0 and 255 representing Red color intensity in RGB color model
 * @property G value between 0 and 255 representing Green color intensity in RGB color model
 * @property B value between 0 and 255 representing Blue color intensity in RGB color model
 */
class LEDData(var x : Int, var y: Int, var R: Int, var G: Int, var B: Int)
