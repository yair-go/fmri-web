There are 3 main cases:
  1. Requested parameters are new. Matlab should calculate the correlation matrix, store it in the matrices cache, create an image, and store it in the images cache.
  1. Requested parameters (except threshold) where already calculated. Matlab should take the stored correlation matrix, create an image, and store it in the images cache.
  1. All requested parameters (including threshold) where already calculated. Matlab is not invoked at all. Image is taken from cache.

# Matlab input #
  * `should_calc_corr_matrix` = 0 / 1 (true / false)
  * case 1:
    * `src_image_filename` (string)
    * `x1`, `x2`, `y1`, `y2`, `z1`, `z2` (integer)
    * `corr_matrix_out_filename` (string)
  * case 0:
    * `corr_matrix_in_filename` (string)
  * `threshold` (double)
  * `corr_image_out_filename` (string)