# Midi2Vol
English | [Español](./README-es.md)





<img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoSlider.png" width="180">  <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoBento.png" width="180"> <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoWavez.png" width="180">



Windows® Volume Control for Nano. Slider -- [Linux](https://github.com/jesusvallejo/Midi2Vol-Linux)

This is mainly developed for Nano. Slider, but it can be fairly easily used with any Midi based potentiometer. 
It is written only for Windows® and wont work on any other plataform as it has to be written in OS compatible language (C# in this case with .net framework).

The app lives in the tray in order to be less intrusive, for the moment the only options are to exit the app and to Add/remove the StartUp run.

I'll provice a binary files but i recommend you to compile it by yourself. 
It is provided as is, and it comes with no guarantee.(see [Licence](https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/LICENSE))

Nevertheless any change, update or upgrade is welcomed.

This project uses the following libraries:

Midi handling:
- NAudio: https://github.com/naudio/NAudio

Volume handling:
- CSCore: https://github.com/filoe/cscore

TODO
- [x] Add auto launch on boot
- [x] Add menu to Apply/Remove auto launch on boot
- [x] Add Hot-Plug support
- [x] Test stability
- [X] Allow control when changing audio output Devices
- [x] Allow per App volume control

