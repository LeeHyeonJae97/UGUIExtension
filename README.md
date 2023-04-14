# UGUIExtension

## Animation
Set animation settings in inspector using DOTween. Only can use with View components.

Punch Effect. Loop alpha of CanvasGroup and Graphic, position, rotation, size. Tween alpha of CanvasGroup and Graphic, position, rotation, size

## Button
Add event for long pressed down and up

## Debug
UI for debug messages during play

## Draggable
Make UI elements draggable

## Joystick
Virtual joystick for mobile games

## LayoutGroup
Auto disable or destroy on play for optimization

## NotificationBadge
Notification icon image for update of contents

## OverscrollChecker
Check over scrolled horizontally or vertically in ScrollView

## PopupText
UI for popup text like error messages

## ScrollView
Pooling slots for ScrollView for optimization. Can loop elements circularly.

## Tab
Simple tab system for mobile games.

## Toggle
Toggle with handle

## View
Simple mobile stackable UI system.
There are four types of View, Fullscreen, Windowed, Popup, Panel. Only one Fullscreen, Windowed, Popup can be used for only one canvas.

If Fullscreen is activated, all views that activated earlier are closed for optimization.

Popup is instantiated when activated and destroyed when deactivated. It's not reused.

Panel is for group of elements inside of View. multiple panels can be use for one canvas.
