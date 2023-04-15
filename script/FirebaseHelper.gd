class_name FirebaseHelper

extends Node

# Declare member variables here. Examples:
# var a = 2
# var b = "text"


var conn : FirestoreCollection
var doc : FirestoreDocument


signal init_count(count)

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
		initFirestore()

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

func initFirestore():
	conn = Firebase.Firestore.collection("counter")
	conn.connect("get_document", self ,"_on_get_document")
	conn.get("TEtTGjL44j3Qz9W4gMeN")
	doc =  yield(conn, "get_document")

func _on_get_document(document : FirestoreDocument) -> void:
	print(document)
	var count = document.doc_fields.get("count")
	emit_signal("init_count", count)
	pass

func updateCount(count):
	if (conn):
		conn.update("TEtTGjL44j3Qz9W4gMeN", {"count": count})
