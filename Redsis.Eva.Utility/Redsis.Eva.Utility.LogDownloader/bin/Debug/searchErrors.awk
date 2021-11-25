# 
# Lineas con indicación de ERROR en ImpresoraOPOS
#
FILENAME != prevfile { 
prevfile = FILENAME 
x=0
}
/INICIO DE APLICACIÓN/ { x=$11 }
/The device is not connected/ || /ImpresoraOPOS ERROR/ {print FILENAME, "Version eva: "x, NR, $0 >> "Errors.txt"} 