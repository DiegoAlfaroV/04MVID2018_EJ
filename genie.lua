local project_list = {
	"AG01",
	"AG02",
	"AG03",
	"AG04",
	"AG05_01",
	"AG05_02",
	"AG06",
	"AG07_01",
	"AG07_02",
	"AG08_01",
	"AG08_02",
	"AG08_03",
	"AG08_04",
	"AG08_05",
	"AG09_01",
	"AG10_01",
	"AG10_02",
	"AG10_03",
	"AG11",
	"AG12",
	"AG13",
	"EJ02_01",
	"EJ02_02",
	"EJ02_03",
	"EJ02_04",
	"EJ02_05",
	"EJ02_06",
	"EJ03_01",
	"EJ03_02",
	"EJ03_03",
	"EJ03_04",
	"EJ04_01",
	"EJ04_02",
	"EJ04_03",
	"EJ04_04",
	"EJ04_05",
	"EJ05_01",
	"EJ05_02",
	"EJ05_03",
	"EJ05_04",
	"EJ05_05",
	"EJ05_06",
	"EJ05_07",
	"EJ05_08",
	"EJ06_01",
	"EJ06_02",
	"EJ06_03",
	"EJ06_04",
	"EJ06_05",
	"EJ06_06",
	"EJ06_07",
	"EJ07_01",
	"EJ07_02",
	"EJ07_03",
	"EJ07_04",
	"EJ07_05",
	"EJ08_01",
	"EJ09_01",
	"EJ10_01",
	"EJ10_02",
	"EJ10_03",
	"EJ10_04",
	"EJ11_01",
	"EJ11_02",
	"EJ12_01",
	"EJ12_02",
	"EJ12_03"
}

local function new_project(name)
	project(name)
		kind("ConsoleApp")
		includedirs{"include", "src/deps/glfw", "src/deps/glad", "src/deps/stb"}
		files{"tests/"..name.."/*.cpp",
			  "tests/"..name.."/*.h",
			  "src/*.cpp",
			  "src/*.cc",
			  "include/**.h",
			  "src/deps/glfw/context.c",
			  "src/deps/glfw/init.c",
			  "src/deps/glfw/input.c",
			  "src/deps/glfw/monitor.c",
			  "src/deps/glfw/window.c",
			  "src/deps/glfw/vulkan.c",
			  "src/deps/glad/glad.c",
			  "src/deps/stb/*"}
		defines {"_GLFW_USE_OPENGL=1"}
		vpaths {["Headers"] = "include/*.h",
				["Source"] = {"src/*.cpp", "src/*.cc"},
				["Deps Headers"] = {"include/GLFW/*",
						    "include/glad/**",
						    "include/KHR/**",
					   	    "include/glm/**"},
				["Deps Sources"] = "src/deps/**",
				["Tests"] = {"tests/"..name.."/*.cpp",
					     "tests/"..name.."/*.h"}}
		configuration{"debug"}
			flags{"Symbols"}
			targetsuffix("_d")
			libdirs{"libs/Debug"}
		configuration{"release"}
			flags("Optimize")
			targetsuffix("_r")
			libdirs{"libs/Release"}
		configuration{"windows"}
			files{"src/deps/glfw/egl_context.c",
				  "src/deps/glfw/win32*",
				  "src/deps/glfw/wgl_*",
				  "src/deps/glfw/winm_*"}
			links{"OpenGL32", "assimp-vc140-mt"}
			defines{"_GLFW_WIN32", "_GLFW_WGL"}
			flags{"NoEditAndcontinue"}
			windowstargetplatformversion "10.0.15063.0"
end

solution "OpenGL_Project"
	configurations { "debug", "release" }
	language ("c++")
	platforms ("x64")
	location ("build")
	debugdir ("build")
	targetdir ("bin")
	flags {"ExtraWarnings"}

for k,v in pairs(project_list) do
	new_project(v)
end
