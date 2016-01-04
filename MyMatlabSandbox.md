# Save matrix to image file (Black & White) #
Say `mtx` is a 2-D matrix of voxel correlations, and we want to save it into `'file.png'`:
```
boolean_mtx = round(abs(mtx));
imwrite(boolean_mtx, 'file.png', 'BitDepth', 1);
```

You can subscruct `threashold - 0.5`, from the matrix abs values, to handle threashold.

# Save matrix to CSV file #
Say `mtx` is a 2-D matrix, and we want to save it into `'file.csv'`:
```
dlmwrite(mtx, 'file.csv');
```

You can subscruct `threashold - 0.5`, from the matrix abs values, to handle threashold.


# Save/Load Matrix to/from a 'mat' file #
Say `mtx` is a 2-D matrix of voxel correlations, and we want to save it into `'myfile.mat'`:
```
save('myfile.mat', 'mtx');
```

Say `'myfile.mat'` is a saved matlab file, that contains `mtx` variable, and we want to load it:
```
load('myfile.mat');
```
(now, `mtx` contains the matrix itself).

# General matrices operations #
To load the FMRI file into Matlab:
```
>> brain = load('Brain.mat')
brain =
    SM: [4-D uint16]
```

Now, you can access the variable `brain.SM`.
```
>> ndims(brain.SM) % number of dimentions
ans =
     4

>> size(brain.SM)
ans =
   132    96    58    80
```


The **time** axis has 132 frames, so the first index specifies the time frame.

To get a sub-cube:
```
>> small_brain = brain.SM(:,[20:22], [20:22], [20:22]);
>> size(small_brain)
ans =
   132     3     3     3
```

To get the image values at a specified time frame (returns a 3-D matrix):
```
>> small_brain(7, :,:,:)
```

To get a vector of values of a specific voxel, along the time:
```
>> voxel1 = brain.SM(:, 20, 20, 20)
voxel1 =
     61
     62
     54
     53
     ...
     50
     68
     64
```

And then..,.
```
>> voxels = [voxel1, voxel2, voxel3 voxel4];
>> corrcoef(double(voxels))
ans =
    1.0000    0.3969    0.2077    0.4931
    0.3969    1.0000    0.5669    0.4104
    0.2077    0.5669    1.0000    0.4327
    0.4931    0.4104    0.4327    1.0000
```

This returns a matrix, represents the correlations between each two voxels. The main diagonal contains `1.0000` values, because it refers the the correlation between each voxel to itself.

**Almost all done! :)**