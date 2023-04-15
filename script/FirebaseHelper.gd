extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():

	print("_ready")

	Firebase.Auth.connect("login_succeeded", self, "_on_FirebaseAuth_login_succeeded")
	Firebase.Auth.connect("signup_succeeded", self, "_on_FirebaseAuth_login_succeeded")
	Firebase.Auth.connect("login_failed", self, "on_login_failed")
	Firebase.Auth.connect("signup_failed", self, "on_signup_failed")
	Firebase.Auth.connect("userdata_received", self, "on_userdata_received")
	Firebase.Auth.connect("auth_request", self, "on_auth_request")

	Firebase.Auth.check_auth_file()

	pass # Replace with function body.


func on_auth_request(err, msg):
	if err && err != 1:
		print(err, msg)
		Firebase.Auth.login_anonymous()
	else:
		print("login succeeded")
		print(msg)

func _on_FirebaseAuth_login_succeeded(auth):
	print("login succeeded", auth)
	Firebase.Auth.save_auth(auth)

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
