-¿Puedes identificar pruebas de unidad y de integración en la práctica que realizaste?
En este caso, las pruebas de unidad que se puede identificar son las que realizamos, como ser:
Probar dar de alta un producto, buscar un producto, eliminar un producto, etc.
Sin embargo, no identifico pruebas de integración ya que tenemos un solo modulo para probar.

¿Podría haber escrito las pruebas primero antes de modificar el código de la aplicación?
¿Cómo sería el proceso de escribir primero los tests?

Si, podría haber escrito los test primero, antes de modificar el codigo, ya que la implementación
de la prueba es independiente de la implementación de los metodos a probar.
Para escribir los test, en Xunit, simplemente llamar al metodo Throws de la clase Assert e indicarle el tipo de excepción que quiero controlar y como parametro el metodo que produce la excepción.
Si se produce la excepción esperada, quiere decir que la prueba fue exitosa.
