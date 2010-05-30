% assumes that the next variables are defined:
% * should_calc_corr_matrix = 0 / 1
% * [case 1:]
%   * src_image_filename (string)
%   * x1, x2, y1, y2, z1, z2, t1, t2 (integer)
%   * corr_matrix_out_filename (string)
% * [case 0:]
%   * corr_matrix_in_filename (string)
% * should_write_xls = 0 / 1
% * [case 1:]
%   * xls_out_filename (string)
%   * zip_out_filename (string)
% * threshold (double)
% * corr_image_out_filename (string)

if ( should_calc_corr_matrix == 1)
    
    load(src_image_filename);
    % SM is now loaded
    
    % x2,y2,z2 should be the upper-range, which is out of count
    x2 = x2+1;
    y2 = y2+1;
    z2 = z2+1;
    subcube = SM([t1:t2], [x1:x2], [y1:y2], [z1:z2]);
    
    [t_range x_range y_range z_range] = size(subcube);
    voxel_count = x_range * y_range * z_range;
    
    % switch from 4-D matrix, to 2-D matrix
    
    voxel_mtx = reshape(subcube, [t_range voxel_count]);
    
    corr_mtx = corrcoef(double(voxel_mtx));
    save(corr_matrix_out_filename, 'corr_mtx');
    
else

    load(corr_matrix_in_filename);
    % corr_mtx in now loaded

end

if ( should_write_xls == 1)

    dlmwrite(xls_out_filename, corr_mtx);
    zip(zip_out_filename, xls_out_filename);

end

boolean_mtx = round(abs(corr_mtx) + (0.5 - threshold));
imwrite(boolean_mtx, corr_image_out_filename, 'BitDepth', 1);
disp('OK');
