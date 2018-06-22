
# C#与golang实现grpc通信示例



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

		



### 使用openssl生成证书及密钥

1.在Windows上开发请安装 OpenSSL对应版本并将[openssl.exe](https://slproweb.com/products/Win32OpenSSL.html)所在路径添加到环境变量中。

2.创建执行文件Generator.bat

	@echo off
	set OPENSSL_CONF=c:\OpenSSL-Win64\bin\openssl.cfg   
	
	echo Generate CA key:
	openssl genrsa -passout pass:1111 -des3 -out ca.key 4096
	
	echo Generate CA certificate:
	openssl req -passin pass:1111 -new -x509 -days 365 -key ca.key -out ca.crt -subj  "/C=US/ST=CA/L=Cupertino/O=YourCompany/OU=YourApp/CN=MyRootCA"
	
	echo Generate server key:
	openssl genrsa -passout pass:1111 -des3 -out server.key 4096
	
	echo Generate server signing request:
	openssl req -passin pass:1111 -new -key server.key -out server.csr -subj  "/C=US/ST=CA/L=Cupertino/O=YourCompany/OU=YourApp/CN=kekyk"
	
	echo Self-sign server certificate:
	openssl x509 -req -passin pass:1111 -days 365 -in server.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out server.crt
	
	echo Remove passphrase from server key:
	openssl rsa -passin pass:1111 -in server.key -out server.key
	
	echo Generate client key
	openssl genrsa -passout pass:1111 -des3 -out client.key 4096
	
	echo Generate client signing request:
	openssl req -passin pass:1111 -new -key client.key -out client.csr -subj  "/C=US/ST=CA/L=Cupertino/O=YourCompany/OU=YourApp/CN=client"
	
	echo Self-sign client certificate:
	openssl x509 -passin pass:1111 -req -days 365 -in client.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out client.crt
	
	echo Remove passphrase from client key:
	openssl rsa -passin pass:1111 -in client.key -out client.key
	pause