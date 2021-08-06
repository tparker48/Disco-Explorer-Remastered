import shutil

src = 'C:\\Users\\Tom\\Desktop\\Disco-Explorer-Remastered\\DiscoExplorer (Remastered)\\bin\Debug\\DiscoExplorer (Remastered).dll'
dest = 'E:\\SteamLibrary\\steamapps\\common\\Disco Elysium\\BepInEx\\plugins\\DiscoExplorer\\DiscoExplorer (Remastered).dll'

shutil.copyfile(src, dest)

exit(0)