packages\Grpc.Tools.1.12.0\tools\windows_x86\protoc.exe -IgRPCDemo --csharp_out gRPCDemo  gRPCDemo\helloworld.proto --grpc_out gRPCDemo --plugin=protoc-gen-grpc=packages\Grpc.Tools.1.12.0\tools\windows_x86\grpc_csharp_plugin.exe
packages\Grpc.Tools.1.12.0\tools\windows_x86\protoc.exe -IgRPCDemo --csharp_out gRPCDemo  gRPCDemo\cc.proto --grpc_out gRPCDemo --plugin=protoc-gen-grpc=packages\Grpc.Tools.1.12.0\tools\windows_x86\grpc_csharp_plugin.exe