extends Label


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():

	Firebase.Auth.connect("login_succeeded", self, "_on_FirebaseAuth_login_succeeded")
	Firebase.Auth.connect("signup_succeeded", self, "_on_FirebaseAuth_login_succeeded")
	Firebase.Auth.connect("login_failed", self, "on_login_failed")
	Firebase.Auth.connect("signup_failed", self, "on_signup_failed")
	Firebase.Auth.connect("userdata_received", self, "on_userdata_received")

	# yield(get_tree().create_timer(3), "timeout")

	Firebase.Auth.login_anonymous()

	pass # Replace with function body.

func _on_FirebaseAuth_login_succeeded(auth):
	print("login succeeded", auth)
	Firebase.Auth.get_user_data()

func on_login_failed(error_code, message):
	print("error code: " + str(error_code))
	print("message: " + str(message))

func on_signup_failed(error_code, message):
	print("error code: " + str(error_code))
	print("message: " + str(message))


func on_userdata_received(userdata):
	print("userdata received", userdata)


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
