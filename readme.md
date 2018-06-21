
# C#与golang实现grpc双向通信示例



编译器下载地址

```
https://repo1.maven.org/maven2/com/google/protobuf/protoc/
```

两个测试proto文件

##cc.proto

	// Copyright 2015 gRPC authors.
	//
	// Licensed under the Apache License, Version 2.0 (the "License");
	// you may not use this file except in compliance with the License.
	// You may obtain a copy of the License at
	//
	//     http://www.apache.org/licenses/LICENSE-2.0
	//
	// Unless required by applicable law or agreed to in writing, software
	// distributed under the License is distributed on an "AS IS" BASIS,
	// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	// See the License for the specific language governing permissions and
	// limitations under the License.
	
	syntax = "proto3";
	
	option java_multiple_files = true;
	option java_package = "io.grpc.examples.helloworld";
	option java_outer_classname = "HelloWorldProto";
	
	package helloworld;
	
	// The greeting service definition.
	service CC {
	  // Sends a greeting
	  rpc SayCC (CCRequest) returns (CCReply) {}
	}
	
	// The request message containing the user's name.
	message CCRequest {
	  string name = 1;
	}
	
	// The response message containing the greetings
	message CCReply {
	  string message = 1;
	}


##helloworld.proto

	// Copyright 2015 gRPC authors.
	//
	// Licensed under the Apache License, Version 2.0 (the "License");
	// you may not use this file except in compliance with the License.
	// You may obtain a copy of the License at
	//
	//     http://www.apache.org/licenses/LICENSE-2.0
	//
	// Unless required by applicable law or agreed to in writing, software
	// distributed under the License is distributed on an "AS IS" BASIS,
	// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	// See the License for the specific language governing permissions and
	// limitations under the License.
	
	syntax = "proto3";
	
	option java_multiple_files = true;
	option java_package = "io.grpc.examples.helloworld";
	option java_outer_classname = "HelloWorldProto";
	
	package helloworld;
	
	// The greeting service definition.
	service Greeter {
	  // Sends a greeting
	  rpc SayHello (HelloRequest) returns (HelloReply) {}
	}
	
	// The request message containing the user's name.
	message HelloRequest {
	  string name = 1;
	}
	
	// The response message containing the greetings
	message HelloReply {
	  string message = 1;
	}


### C#项目测试

参考地址 <https://www.cnblogs.com/linezero/p/grpc.html>

1.新建项目
2.添加引用 

	Install-Package Grpc
	Install-Package Google.Protobuf
	Install-Package Grpc.Tools

3.生成c#代码文件
	
	packages\Grpc.Tools.1.12.0\tools\windows_x86\protoc.exe -IgRPCDemo --csharp_out gRPCDemo  gRPCDemo\helloworld.proto --grpc_out gRPCDemo --plugin=protoc-gen-grpc=packages\Grpc.Tools.1.12.0\tools\windows_x86\grpc_csharp_plugin.exe
	packages\Grpc.Tools.1.12.0\tools\windows_x86\protoc.exe -IgRPCDemo --csharp_out gRPCDemo  gRPCDemo\cc.proto --grpc_out gRPCDemo --plugin=protoc-gen-grpc=packages\Grpc.Tools.1.12.0\tools\windows_x86\grpc_csharp_plugin.exe

4.编写测试用例,见源码


### Go项目测试

1.生成go代码文件

	protoc -I helloworld/ helloworld/helloworld.proto --go_out=plugins=grpc:helloworld
	protoc -I helloworld/ helloworld/cc.proto --go_out=plugins=grpc:helloworld

2.编写测试用例,见源码

		
