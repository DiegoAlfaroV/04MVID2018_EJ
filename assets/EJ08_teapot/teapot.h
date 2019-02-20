#ifndef TEAPOT_H
#define TEAPOT_H

#include <GL/glew.h>
#include <glm/glm.hpp>

class Teapot {
public:
  Teapot();
  unsigned int vbo[4];

  void Init(int grid, mat4 lidTransform);

  void render();
private:
  GLuint _vao;
  unsigned int faces;

  void generatePatches(float * v, float * n, float *tc, unsigned int* el, int grid);
  void buildPatchReflect(int patchNum, float *B, float *dB,
    float *v, float *n, float *, unsigned int *el,
    int &index, int &elIndex, int &, int grid,
    bool reflectX, bool reflectY);
  void buildPatch(glm::vec3 patch[][4], float *B, float *dB,
    float *v, float *n, float *, unsigned int *el,
    int &index, int &elIndex, int &, int grid, glm::mat3 reflect, bool invertNormal);
  void getPatch(int patchNum, glm::vec3 patch[][4], bool reverseV);

  void computeBasisFunctions(float * B, float * dB, int grid);
  glm::vec3 evaluate(int gridU, int gridV, float *B, glm::vec3 patch[][4]);
  glm::vec3 evaluateNormal(int gridU, int gridV, float *B, float *dB, glm::vec3 patch[][4]);
  void moveLid(int, float *, glm::mat4);
protected:
  ~Teapot();
};