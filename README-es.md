# Midi2Vol
[English](./README.md)| Español



<img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoSlider.png" width="180">  <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoBento.png" width="180"> <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoWavez.png" width="180">



Control de Volumen de Windows® para Nano. Slider -- [Linux](https://github.com/jesusvallejo/Midi2Vol-Linux)



Este proyecto esta desarrollado en principio para [Nano. Slider](https://www.keebwerk.com/nano-slider/), sin embargo debería ser realmente sencillo crear una versión para cualquier pontenciómetro que funcione via MIDI.
Esta aplicacion funciona solo en Windows®([Version para Linux](https://github.com/jesusvallejo/Midi2Vol-Linux)).

La aplicación se ejecuta en la bandeja de iconos para ser menos intrusiva.

Esta version tiene control de volumen para aplicaciones(Usa el mixer de windows y puede controlar el volumen de aplicaciones como Spotify o Google Chrome) y dispositivos(Además puede controlar el volumen del dispositivo del sistema que reproduce sonido en ese momento, auriculares, altavoces).

Estos ajustes se pueden modificar usando el boton config o editando el archivo config.json(se encuentra en el directorio 
```\user\AppData\midi2vol\```, se recomienda usar el menu para evitar problemas de parsing en el archivo).

Se incluyen [binarios ](https://github.com/jesusvallejo/Midi2Vol/releases) para su instalación, sin embargo recomiendo que lo compiles tu mismo.
Los binarios vienen sin firmar ya que el certificado cuesta anualmente.

Se provee tal cual esta , y carece de cualquier tipo de garantia.(ver [Licencia](https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/LICENSE))

Siempre se agradece cualquier mejora o propuesta.

Este projecto usa las siguientes librerias:

Midi handling:
- NAudio: https://github.com/naudio/NAudio

Volume handling:
- CSCore: https://github.com/filoe/cscore

TODO
- [ ] Check wether the AppRaw input in config menu is an hex.
- [ ] Make it easier for user to change app icons.(not sure if possible)
- [ ] Check .json parsing errors
