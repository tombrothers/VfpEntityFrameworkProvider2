rd /q /s bin
rd /q /s obj
del /A:H *.suo
pushd ..
del EFQuerySamples.zip
zip -9 -X -r EFQuerySamples.zip EFQuerySamples
popd
