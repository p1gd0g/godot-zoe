extends Label

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

var _count : int = 0

var helperPath := "/root/Node/FirebaseHelper"

onready var helper : FirebaseHelper = get_node(helperPath) as FirebaseHelper

func _on_FirebaseHelper_init_count(count):
	_count = count
	self.text = "你接住了 %d 个曾晴！" % _count
	pass # Replace with function body.

func on_do_count():
	_count = _count + 1
	self.text = "你接住了 %d 个曾晴！" % _count
	helper.updateCount(_count)

