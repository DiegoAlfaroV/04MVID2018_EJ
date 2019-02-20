#ifndef __MODEL_H__
#define __MODEL_H__ 1

#include <string>
#include "mesh.h"
#include "assimp\material.h"

class aiNode;
struct aiScene;
class aiMesh;
class aiMaterial;

class Model {
public:
	/* Model Data */
	std::vector<Texture> textures_loaded_;
	std::vector<Mesh> meshes_;
	std::string directory_;
	bool gammaCorrection_;

	/* Functions */
	//Constructor, expects a filepath to a 3D model.
	Model(std::string const &path, bool gamma = false);

	//Draws the model, and thus all its meshes
	void Draw(const Shader& shader) const;

private:
	void loadModel(std::string const path);

	void processNode(aiNode *node, const aiScene *scene);
	Mesh processMesh(aiMesh *mesh, const aiScene *scene);

	std::vector<Texture> loadMaterialTextures(aiMaterial *mat, aiTextureType type, std::string typeName);
};

#endif