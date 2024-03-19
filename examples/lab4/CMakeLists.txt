project("FirstLab")

set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_STANDARD 20)

set(source
    main.cpp
    CsvHelper.cpp
    SongSearch.cpp
    Song.cpp
)

set(heaers
    CsvHelper.h
    SongSearch.h
    Song.h
)

add_executable(SongSearcher ${source} ${heaers})
