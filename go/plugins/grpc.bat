protoc -I helloworld/ helloworld/helloworld.proto --go_out=plugins=grpc:helloworld
protoc -I helloworld/ helloworld/cc.proto --go_out=plugins=grpc:helloworld