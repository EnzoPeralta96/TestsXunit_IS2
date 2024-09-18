¿Puedes identificar pruebas de unidad y de integración en la práctica que realizaste?

En el contexto de TiendaTest.cs, podemos distinguir entre pruebas unitarias y pruebas de integración en función de su alcance y dependencias.
Pruebas unitarias
Las pruebas unitarias se centran en probar componentes o métodos individuales de forma aislada, a menudo usando mocks para simular dependencias. Estas son las pruebas unitarias de TiendaTest.cs:

1.	AgregarProducto
2.	BuscarProducto
3.	EliminarProducto
4.	ModificarPrecio
5.	Aplicar_descuento
6.	Exception Tests:
•	AgregarProducto_Exception
•	BuscarProducto_Exception
•	EliminarProducto_Exception
•	ModificarPrecio_Exception
	
7.	Mock Tests:
•	Aplicar_descuento_UtilizandoMock
•	AgregarProducto_UtilizandoMock
•	EliminarProducto_UtilizandoMock

Pruebas de integración
Las pruebas de integración se centran en probar la interacción entre varios componentes o sistemas para garantizar que funcionen juntos como se espera. Aquí está la prueba de integración de TiendaTest.cs:

1.	IntegracionTienda

Esta prueba implica varias operaciones:
• Añadir productos a la tienda.
• Aplicar un descuento a un producto.
• Retirar un producto.
• Calcular el precio total de un carrito de compras.
Prueba la interacción entre estas operaciones, lo que lo convierte en una prueba de integración.


¿Podría haber escrito las pruebas primero antes de modificar el código de la aplicación?
¿Cómo sería el proceso de escribir primero los tests?

Sí, escribir pruebas antes de modificar el código de la aplicación es una práctica conocida como Test-Driven Development (TDD). TDD es un enfoque de desarrollo de software en el que se escriben pruebas para una nueva característica o funcionalidad antes de escribir el código de implementación real. 
Este enfoque ayuda a garantizar que el código cumpla los requisitos especificados y se comporte según lo esperado.
Pasos para TDD
1. Escribir una prueba: Escriba una prueba para la nueva funcionalidad que desea agregar. Inicialmente, se debe producir un error en la prueba porque la funcionalidad aún no está implementada.
2. Ejecute la prueba: Ejecute la prueba para asegurarse de que falle. Esto confirma que la prueba está identificando correctamente la ausencia de la funcionalidad deseada.
3. Escriba el código: Escriba la cantidad mínima de código necesaria para que la prueba pase.
4. Vuelva a ejecutar la prueba: Vuelva a ejecutar la prueba para asegurarse de que se apruebe con el nuevo código.
5. Refactorizar: Refactorizar el código para mejorar su estructura y legibilidad mientras se asegura de que la prueba aún se apruebe.
6. Repite: Repita el proceso para la siguiente pieza de funcionalidad.

Para escribir los test, en Xunit, simplemente llamar al metodo Throws de la clase Assert e indicarle el tipo de excepción que quiero controlar y como parametro el metodo que produce la excepción.
Si se produce la excepción esperada, quiere decir que la prueba fue exitosa.

En lo que va del trabajo práctico, ¿puedes identificar Drivers y Stubs?

En el contexto de las pruebas unitarias, los Drivers y los Stubs son tipos de dobles de prueba que se usan para aislar la unidad de código que se está probando.

Drivers

Los controladores son componentes de un fragmento de código que invocan la unidad de código que se está probando. 
A menudo se utilizan para simular componentes de nivel superior que llaman a los métodos de la unidad que se está probando.
En el contexto de TiendaTest.cs, los propios métodos de prueba actúan como drivers porque llaman a los métodos de la clase Tienda.

Stubs

Los stubs son implementaciones simplificadas de componentes de los que depende la unidad sometida a prueba.
Proporcionan respuestas controladas a la unidad bajo prueba, lo que le permite aislar el comportamiento de la unidad.
En TiendaTest.cs, el uso de la biblioteca Moq para crear objetos simulados se puede considerar como el uso de stubs.

Identificacion de Drivers and Stubs en TiendaTest.cs

Los métodos de prueba en TiendaTest.cs actúan como drivers. Por ejemplo:

[Fact]
public void AgregarProducto()
{
    // Given
    var productoNuevo = new Producto("Arroz", 500, Categoria.NoPerecedero);
    
    // When
    _fixture.Tienda.AgregarProducto(productoNuevo);
    var productoBuscado = _fixture.Tienda.BuscarProducto("Arroz");

    // Then
    Assert.NotNull(productoBuscado);
    Assert.Equal(productoNuevo, productoBuscado);
}

En este método de prueba:
El propio método de prueba es el driver porque invoca los métodos AgregarProducto y BuscarProducto de la clase Tienda.

Stubs

El uso de la biblioteca Moq para crear objetos mock sirve como stubs. Por ejemplo:

[Fact]
public void Aplicar_descuento_UtilizandoMock()
{
    // Given
    var mockProducto = new Mock<Producto>("Leche", 1000, Categoria.Perecedero);
    mockProducto.Setup(p => p.Precio).Returns(900);

    var tienda = new Tienda();
    tienda.AgregarProducto(mockProducto.Object);

    // When
    tienda.AplicarDescuento("Leche", 10);
    var productoBuscado = tienda.BuscarProducto("Leche");

    // Then
    Assert.Equal(900, productoBuscado.Precio);
}

En este método de prueba:
El objeto mockProducto creado con Moq actúa como un stub para el objeto Producto real. proporciona una respuesta controlada para la propiedad Precio, lo que permite que la prueba se centre en el comportamiento del método aplicarDescuento.

Conclusion
•	Drivers: Los propios métodos de prueba actúan como drivers invocando los métodos de la clase Tienda.
•	Stubs: Los objetos mock creados con Moq actúan como stubs proporcionando respuestas controladas para las dependencias de la clase Tienda.

¿Qué es un mock? 

Un mock es un tipo de doble prueba que se utiliza en las pruebas unitarias para simular el comportamiento de objetos reales. Los mocks se utilizan para aislar la unidad de código que se está probando de sus dependencias externas, como bases de datos, servicios web o componentes de terceros.
Son particularmente útiles cuando los objetos reales son complejos, lentos o tienen efectos secundarios que desea evitar durante las pruebas.

Características clave de los Mocks

1. Simulación de comportamiento: Los Mocks se pueden programar para devolver valores específicos o generar excepciones cuando se llama a ciertos métodos.
2. Verificación de interacción: Los Mocks pueden verificar que se llamó a ciertos métodos con argumentos específicos, cuántas veces se llamaron y en qué orden.
3. Aislamiento: Los Mocks ayudan a aislar la unidad bajo prueba reemplazando sus dependencias con un comportamiento controlado y predecible.

Los Mocks son herramientas eficaces de las pruebas unitarias que ayudan a aislar la unidad que se está probando simulando el comportamiento de sus dependencias.
Permiten controlar las respuestas y verificar las interacciones, lo que hace que las pruebas sean más confiables y enfocadas.

¿Hay otros nombres para los objetos/funciones simulados?

Hay varios términos alternativos para objetos o funciones que se simulan en el contexto de las pruebas. Estos términos a menudo se usan indistintamente, pero pueden tener significados ligeramente diferentes según sus roles y comportamientos específicos. 
Estos son algunos términos comunes:
Test Doubles

Test double es un término genérico que engloba todos los tipos de objetos o funciones simulados utilizados en las pruebas.
1.	Mocks:
•	Simulan el comportamiento de objetos reales
•	Pueden verificar las interacciones (e.g., method calls, arguments).
•	Ejemplo: Usar Moq para crear un objeto mock en C#. 

2.	Stubs:
•	Proporciona respuestas predefinidas a las llamadas a métodos.
•	No verifica las interacciones.
•	Ejemplo: Un método Hardcodeado que devuelve un valor específico.

3.	Fakes:
•	proporcionan una implementación de trabajo, pero son más simples que el objeto real.
•	A menudo se usa para bases de datos en memoria o versiones simplificadas de servicios.
•	Ejemplo: Una implementación en memoria de una interfaz de repositorio.

4.	Spies:
•	Similar a los Mocks, pero también registran información sobre cómo son llamados.
•	Se puede utilizar para verificar las interacciones a posteriori.
•	Ejemplo: Un spy que registra métodos, llamadas y argumentos para su posterior verificación.

5.	Dummies:
•	Normalmente se utiliza para rellenar listas de parámetros.
•	Ejemplo: Un objeto ficticio pasado a un método que no interactúa con él.

Comprender estos diferentes tipos de test doubles ayuda a elegir la herramienta adecuada para sus necesidades de pruebas. Cada tipo tiene un propósito específico y se puede usar para aislar la unidad bajo prueba, controlar su entorno y verificar su comportamiento.