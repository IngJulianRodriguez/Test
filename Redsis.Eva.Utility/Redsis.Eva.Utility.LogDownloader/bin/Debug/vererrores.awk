{name = substr(FILENAME,1,index(FILENAME,";")-1)}
/ERROR/ && !/tablaDescuento/{
 print  name, NR, $0 >> "Errors"date".txt"
}