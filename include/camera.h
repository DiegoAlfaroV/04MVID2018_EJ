#ifndef __CAMERA_H__
#define __CAMERA_H__ 1

#include <glad\glad.h>
#include <glm\glm.hpp>
#include <glm\gtc\matrix_transform.hpp>

//Default Camera Values
const float k_Yaw = -90.0f;
const float k_Pitch = 0.0f;
const float k_Speed = 2.5f;
const float k_Sensitivity = 0.1f;
const float k_FOV = 45.0f;

class Camera {
public:
	enum class Movement { //Possible options for camera movement
		Forward = 0,
		Backward = 1,
		Left = 2,
		Right = 3,
	};
	Camera(); //Empty Constructor
	~Camera(); //Destructor
	Camera(const glm::vec3 position = glm::vec3(0.0f, 0.0f, 0.0f),
		const glm::vec3 up = glm::vec3(0.0f, 1.0f, 0.0f),
		const float yaw = k_Yaw, const float pitch = k_Pitch);
	//Constructor with Scalars
	Camera(const float posX, const float posY, const float posZ,
		const float upX, const float upY, const float upZ,
		const float yaw, const float pitch);
	
	glm::mat4 getViewMatrix() const; //Returns current view matrix
	glm::mat4 getViewMatrixNew() const; //Returns current view matrix
	float getFOV() const; //Returns the FOV
	glm::vec3 getPosition() const;
						  
						  //Process input from keyboard
	void handleKeyboard(const Movement direction, const float deltaTime);
	//Process mouse movement
	void handleMouseMovement(const float xoffset, const float yoffset,
		const bool constrainPitch = true);
	//Process mouse scroll
	void handleMouseScroll(const float yoffset);

	void setFlying(const bool flying);
private:
	void updateCameraVectors(); //Calculates front vector

	glm::mat4 lookAt() const;
	glm::mat4 newLookAt() const;

	glm::vec3 position_, front_, up_, right_, worldUp_; // Camera Attributes
	float yaw_, pitch_; // Euler Angles
	float fov_; // Camera options

	bool flying_ = true;	//Camera can fly
};

#endif