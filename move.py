import inspect
import os
import re
import shutil
import winreg

from pathlib import Path

THIS_FILE = Path(os.path.abspath(inspect.getsourcefile(lambda: None) or __file__))
THIS_DIR = THIS_FILE.parent

# Steam AppID for Disco Elysium / The Final Cut
DISCO_ELYSIUM_APP_ID = 632470
# Conventional steamapps\common folder name, used if the manifest is unreadable.
DISCO_ELYSIUM_INSTALL_DIR_NAME = "Disco Elysium"
GAME_EXE_NAME = "disco.exe"

def get_disco_install_dir():
    install_dir = os.environ.get("DISCO_ELYSIUM_INSTALL_DIR")
    if install_dir:
        return Path(install_dir)
    return find_steam_game_dir()


def get_steam_path():
    probes = [
        {"hive": winreg.HKEY_CURRENT_USER, "sub_key": r"Software\Valve\Steam", "value_name": "SteamPath"},
        {"hive": winreg.HKEY_LOCAL_MACHINE, "sub_key": r"SOFTWARE\WOW6432Node\Valve\Steam", "value_name": "InstallPath"}
    ]

    for probe in probes:
        try:
            with winreg.OpenKey(probe["hive"], probe["sub_key"]) as key:
                value, _ = winreg.QueryValueEx(key, probe["value_name"])
                if value:
                    return value
        except FileNotFoundError:
            continue
    return None


def get_steam_library_folders(steam_path: str):
    if not steam_path:
        return []

    roots = []
    seen = set()

    def add_root(path_str):
        # Clean up separators, convert to absolute path, and handle casing
        normalized = str(Path(path_str).resolve())
        if normalized.lower() not in seen:
            seen.add(normalized.lower())
            roots.append(normalized)

    add_root(steam_path)

    relative_vdf_paths = [
        os.path.join("config", "libraryfolders.vdf"),
        os.path.join("steamapps", "libraryfolders.vdf")
    ]

    for rel in relative_vdf_paths:
        vdf_path = os.path.join(steam_path, rel)

        if not os.path.exists(vdf_path):
            continue

        try:
            with open(vdf_path, "r", encoding="utf-8", errors="ignore") as f:
                text = f.read()

            matches = re.findall(r'"path"\s*"([^"]+)"', text)

            for path_match in matches:
                # VDF files double-escape backslashes ("\\\\").
                unescaped_path = path_match.replace(r"\\", "\\")
                if unescaped_path:
                    add_root(unescaped_path)

        except Exception:
            continue

    return roots


def find_steam_game_dir() -> Path | None:
    steam_path = get_steam_path()
    if not steam_path:
        return None

    for lib in get_steam_library_folders(steam_path):
        lib_path = Path(lib)
        install_dirs = []

        acf_path = lib_path / f"steamapps/appmanifest_{DISCO_ELYSIUM_APP_ID}.acf"
        if acf_path.exists():
            try:
                content = acf_path.read_text(encoding="utf-8")
                match = re.search(r'"installdir"\s*"([^"]+)"', content)
                if match:
                    install_dirs.append(match.group(1))
            except IOError:
                pass

        install_dirs.append(DISCO_ELYSIUM_INSTALL_DIR_NAME)

        for install_dir in install_dirs:
            game_dir = lib_path / "steamapps" / "common" / install_dir
            if (game_dir / GAME_EXE_NAME).exists():
                return game_dir

    return None

PLUGIN_DLL_NAME = "DiscoExplorer (Remastered).dll"

disco_install_dir = get_disco_install_dir()

if not disco_install_dir.is_dir():
    raise RuntimeError(f"Could not find Disco Explorer install dir (perhaps set $DISCO_EXPLORER_INSTALL_DIR?) - expected it at: {disco_install_dir} ")

src = THIS_DIR / "DiscoExplorer (Remastered)" / "bin" / "Debug" / "net6.0" / PLUGIN_DLL_NAME
dest = disco_install_dir / "BepInEx" / "plugins" / PLUGIN_DLL_NAME

dest.parent.mkdir(exist_ok=True, parents=True)

shutil.copyfile(src, dest)

exit(0)