Goal: Find all batch files that are being ran everyday from an automated system.

Given: List of programs ran daily and the corresponding batch files.

Required Output: A list with only the batch files that are actually being ran everyday in a very large directory of batch files.

Steps: 
1. Open text file, parse it, and create an array/object of filenames and types to match on.

2. Input search criteria and known directories into application.

3. For each found append to an array its matching search criteria, the filepath, and filename.

4. Return array in either text or excel spreadsheet.