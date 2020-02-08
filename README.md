# Midi2Vol
Windows Volume Control for Nano. Slider

![alt text]https://github.com/jesusvallejo/Midi2Vol/blob/master/NanoSlider.ico)
Windows midi to volume converter for Nano. slider

This is mainly developed for Nano. Slider, but it can be fairly easily used with any Midi based potentiometer. 
It is written only for Windows and wont work on any other plataform as it has to be written in OS compatible language (C# in this case with .net framework).

I'll provice a binary installer but i recommend you to compile it by yourself. 
It is provided as is , and it comes with no guarantee. 
Nevertheless any change is welcomed and will try to make it as efficient as possible.



This uses the following libraries:
           - AudioSwitcher.AudioApi.CoreAudio : https://github.com/xenolightning/AudioSwitcher
           - NAudio: https://github.com/naudio/NAudio

Not needed anymore.

~~In order to make it work you'll have to edit in config.#define PRODUCT keyboard into #define PRODUCT Nano. Slider~~

