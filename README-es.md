# Midi2Vol
[English](./README.md)| Español



<img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoSlider.png" width="180">  <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoBento.png" width="180"> <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoWavez.png" width="180">



Control de Volumen de Windows para Nano. Slider



Este proyecto esta desarrollado en principio para Nano. Slider, sin embargo debería ser realmente sencillo crear una versión para cualquier pontenciometro que funcione via MIDI.
Esta aplicacion funciona solo en windows y en consecuencia no funcionará en ninguna otra plataforma, esto se debe a que ha sido escrita especificamente en C# para poder controlar el volumen del equipo.

La aplicación se ejecuta en la bandeja de iconos para ser menos intrusiva, por el momento las únicas opciones son Exit(para cerrar la aplicación) y Add/remove the StartUp run(para añadir o eliminar la ejecución durante el encendido).
Durante la instalacion la aplicacion sera añadida para su ejecución durante el encendido por defecto, en caso de no querer que esto ocurra podra eliminarla facilmente con la opcion Add/remove the StartUp run.

Existen tres versiones de la aplicación, las tres vesiones son exactamente iguales a excepción del icono mostrado, de modo que la aplicación se puede parecer a tu Nano. Slider.

Se incluye un binario para su instalación, sin embargo recomiendo que lo compiles tu mismo.
Se provee tal cual esta , y carece de cualquier garantia.(ver [Licencia](https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/LICENSE))

Siempre se agradece cualquier mejora o propuesta.

Este projecto usa las siguientes librerias:

- AudioSwitcher.AudioApi.CoreAudio : https://github.com/xenolightning/AudioSwitcher
- NAudio: https://github.com/naudio/NAudio


TODO
- [x] Msi installer
- [x] Edition Icon
- [x] Separate classes into multiple files to improve readability
- [x] Add auto launch on boot
- [x] Add menu to Apply/Remove auto launch on boot
- [x] Add Hot-Plug support
- [x] Solved Bug: wont work after sleep mode
- [ ] One Msi installer, multiple icons that can be selected to fit any Nano. Slider appearance
- [ ] Ask on install , whether to run on Startup or not
- [x] Set proper public/private flags to code
- [x] Test stability
