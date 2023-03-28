using Godot;
using System;

public class VirtualJoystick : Control
{

    // export(Color) var pressed_color := Color.gray
    [Export]
    public Color pressed_color = Colors.Gray;

    // export(float, 0, 200, 1) var deadzone_size : float = 10
    [Export]
    public float deadzone_size = 10;


    // export(float, 0, 500, 1) var clampzone_size : float = 75
    [Export]
    public float clampzone_size = 75;

    // enum JoystickMode {FIXED, DYNAMIC}
    public enum JoystickMode
    {
        FIXED, DYNAMIC
    }


    // export(JoystickMode) var joystick_mode := JoystickMode.FIXED
    [Export]
    public JoystickMode joystick_mode = JoystickMode.FIXED;

    // enum VisibilityMode {ALWAYS , TOUCHSCREEN_ONLY }
    public enum VisibilityMode
    {
        ALWAYS, TOUCHSCREEN_ONLY
    }

    // export(VisibilityMode) var visibility_mode := VisibilityMode.ALWAYS
    [Export]
    public VisibilityMode visibility_mode = VisibilityMode.ALWAYS;

    // export var use_input_actions := true
    [Export]
    public bool use_input_actions = true;


    // export var action_left := "ui_left"
    // export var action_right := "ui_right"
    // export var action_up := "ui_up"
    // export var action_down := "ui_down"

    [Export]
    public string action_left = "ui_left";
    [Export]
    public string action_right = "ui_right";
    [Export]
    public string action_up = "ui_up";
    [Export]
    public string action_down = "ui_down";


    // var _pressed := false setget , is_pressed

    bool _pressed = false;

    public bool _isPressed
    {
        get
        {
            return _pressed;
        }
    }

    // var _output := Vector2.ZERO setget , get_output
    Vector2 _output = Vector2.Zero;

    public Vector2 _getOutput
    {
        get
        {
            return _output;
        }
    }

    // var _touch_index : int = -1
    int _touch_index = -1;

    // onready var _base := $Base
    Control _base;
    // onready var _tip := $Base/Tip
    Control _tip;
    // onready var _base_radius = _base.rect_size * _base.get_global_transform_with_canvas().get_scale() / 2
    Vector2 _base_radius;

    // onready var _base_default_position : Vector2 = _base.rect_position
    // onready var _tip_default_position : Vector2 = _tip.rect_position

    Vector2 _base_default_position;
    Vector2 _tip_default_position;


    // onready var _default_color : Color = _tip.modulate
    Color _default_color;

    void onReady()
    {
        _base = GetNode<Control>("Base");
        _tip = GetNode<Control>("Base/Tip");
        _base_radius = _base.RectSize * _base.GetGlobalTransformWithCanvas().Scale / 2;

        _base_default_position = _base.RectPosition;
        _tip_default_position = _tip.RectPosition;

        _default_color = _tip.Modulate;
    }


    public override void _Ready()
    {
        onReady();

        if (!OS.HasTouchscreenUiHint() && visibility_mode == VisibilityMode.TOUCHSCREEN_ONLY)
        {
            Hide();
        }
    }


    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventScreenTouch event_screen_touch)
        {
            if (event_screen_touch.Pressed)
            {

                if (isPointInsideJoystickArea(event_screen_touch.Position) && _touch_index == -1)
                {
                    // if joystick_mode == JoystickMode.DYNAMIC or (joystick_mode == JoystickMode.FIXED and _is_point_inside_base(event.position)):
                    if (joystick_mode == JoystickMode.DYNAMIC || (joystick_mode == JoystickMode.FIXED && isPointInsideBase(event_screen_touch.Position)))
                    {
                        // if joystick_mode == JoystickMode.DYNAMIC:
                        // 	_move_base(event.position)
                        if (joystick_mode == JoystickMode.DYNAMIC)
                        {
                            moveBase(event_screen_touch.Position);
                        }
                        // _touch_index = event.index
                        _touch_index = event_screen_touch.Index;
                        // _tip.modulate = pressed_color
                        _tip.Modulate = pressed_color;
                        // _update_joystick(event.position)
                        updateJoystick(event_screen_touch.Position);
                        // get_tree().set_input_as_handled()
                        GetTree().SetInputAsHandled();
                    }
                }
            }
            else if (event_screen_touch.Index == _touch_index)
            {
                // _reset()
                reset();
                // get_tree().set_input_as_handled()
                GetTree().SetInputAsHandled();
            }
        }
        else if (inputEvent is InputEventScreenDrag event_screen_drag)
        {
            if (event_screen_drag.Index == _touch_index)
            {
                // _update_joystick(event.position)
                updateJoystick(event_screen_drag.Position);
                // get_tree().set_input_as_handled()
                GetTree().SetInputAsHandled();
            }
        }
    }

    bool isPointInsideJoystickArea(Vector2 point)
    {
        // var x: bool = point.x >= rect_global_position.x and point.x <= rect_global_position.x + (rect_size.x * get_global_transform_with_canvas().get_scale().x)
        var x = point.x >= RectGlobalPosition.x && point.x <= RectGlobalPosition.x + (RectSize.x * GetGlobalTransformWithCanvas().Scale.x);
        // var y: bool = point.y >= rect_global_position.y and point.y <= rect_global_position.y + (rect_size.y * get_global_transform_with_canvas().get_scale().y)
        var y = point.y >= RectGlobalPosition.y && point.y <= RectGlobalPosition.y + (RectSize.y * GetGlobalTransformWithCanvas().Scale.y);

        // return x and y
        return x && y;
    }


    // _is_point_inside_base
    bool isPointInsideBase(Vector2 point)
    {
        //  var center : Vector2 = _base.rect_global_position + _base_radius
        // 	var vector : Vector2 = point - center
        // 	if vector.length_squared() <= _base_radius.x * _base_radius.x:
        // 		return true
        // 	else:
        // 		return false

        var center = _base.RectGlobalPosition + _base_radius;
        var vector = point - center;
        if (vector.LengthSquared() <= _base_radius.x * _base_radius.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // _move_base
    void moveBase(Vector2 point)
    {
        // _base.rect_global_position = new_position - _base.rect_pivot_offset * get_global_transform_with_canvas().get_scale()
        _base.RectGlobalPosition = point - _base.RectPivotOffset * GetGlobalTransformWithCanvas().Scale;
    }


    void updateJoystick(Vector2 point)
    {
        // var center : Vector2 = _base.rect_global_position + _base_radius
        // var vector : Vector2 = touch_position - center
        // vector = vector.limit_length(clampzone_size)

        var center = _base.RectGlobalPosition + _base_radius;
        var vector = point - center;
        vector = vector.LimitLength(clampzone_size);

        // _move_tip(center + vector)

        moveTip(center + vector);

        // if vector.length_squared() > deadzone_size * deadzone_size:
        // 	_pressed = true
        // 	_output = (vector - (vector.normalized() * deadzone_size)) / (clampzone_size - deadzone_size)
        // else:
        // 	_pressed = false
        // 	_output = Vector2.ZERO

        if (vector.LengthSquared() > deadzone_size * deadzone_size)
        {
            _pressed = true;
            _output = (vector - (vector.Normalized() * deadzone_size)) / (clampzone_size - deadzone_size);
        }
        else
        {
            _pressed = false;
            _output = Vector2.Zero;
        }

        // if use_input_actions:
        // 	_update_input_actions()

        if (use_input_actions)
        {
            updateInputActions();
        }
    }

    void moveTip(Vector2 point)
    {
        GD.Print("moveTip", point);
        // _tip.rect_global_position = new_position - _tip.rect_pivot_offset * _base.get_global_transform_with_canvas().get_scale()
        _tip.RectGlobalPosition = point - _tip.RectPivotOffset * _base.GetGlobalTransformWithCanvas().Scale;
    }


    void updateInputActions()
    {
        // if _output.x < 0:
        // 	Input.action_press(action_left, -_output.x)
        // elif Input.is_action_pressed(action_left):
        // 	Input.action_release(action_left)
        // if _output.x > 0:
        // 	Input.action_press(action_right, _output.x)
        // elif Input.is_action_pressed(action_right):
        // 	Input.action_release(action_right)
        // if _output.y < 0:
        // 	Input.action_press(action_up, -_output.y)
        // elif Input.is_action_pressed(action_up):
        // 	Input.action_release(action_up)
        // if _output.y > 0:
        // 	Input.action_press(action_down, _output.y)
        // elif Input.is_action_pressed(action_down):
        // 	Input.action_release(action_down)

        if (_output.x < 0)
        {
            Input.ActionPress(action_left, -_output.x);
        }
        else if (Input.IsActionPressed(action_left))
        {
            Input.ActionRelease(action_left);
        }
        if (_output.x > 0)
        {
            Input.ActionPress(action_right, _output.x);
        }
        else if (Input.IsActionPressed(action_right))
        {
            Input.ActionRelease(action_right);
        }
        if (_output.y < 0)
        {
            Input.ActionPress(action_up, -_output.y);
        }
        else if (Input.IsActionPressed(action_up))
        {
            Input.ActionRelease(action_up);
        }
        if (_output.y > 0)
        {
            Input.ActionPress(action_down, _output.y);
        }
        else if (Input.IsActionPressed(action_down))
        {
            Input.ActionRelease(action_down);
        }

    }

    void reset()
    {
        // _pressed = false
        _pressed = false;
        // _output = Vector2.ZERO
        _output = Vector2.Zero;
        // _touch_index = -1
        _touch_index = -1;
        // _tip.modulate = _default_color
        _tip.Modulate = _default_color;
        // _base.rect_position = _base_default_position
        _base.RectPosition = _base_default_position;
        // _tip.rect_position = _tip_default_position
        _tip.RectPosition = _tip_default_position;
        // if use_input_actions:
        // 	if Input.is_action_pressed(action_left) or Input.is_action_just_pressed(action_left):
        // 		Input.action_release(action_left)
        // 	if Input.is_action_pressed(action_right) or Input.is_action_just_pressed(action_right):
        // 		Input.action_release(action_right)
        // 	if Input.is_action_pressed(action_down) or Input.is_action_just_pressed(action_down):
        // 		Input.action_release(action_down)
        // 	if Input.is_action_pressed(action_up) or Input.is_action_just_pressed(action_up):
        // 		Input.action_release(action_up)

        if (use_input_actions)
        {
            if (Input.IsActionPressed(action_left) || Input.IsActionJustPressed(action_left))
            {
                Input.ActionRelease(action_left);
            }
            if (Input.IsActionPressed(action_right) || Input.IsActionJustPressed(action_right))
            {
                Input.ActionRelease(action_right);
            }
            if (Input.IsActionPressed(action_down) || Input.IsActionJustPressed(action_down))
            {
                Input.ActionRelease(action_down);
            }
            if (Input.IsActionPressed(action_up) || Input.IsActionJustPressed(action_up))
            {
                Input.ActionRelease(action_up);
            }
        }

    }

}