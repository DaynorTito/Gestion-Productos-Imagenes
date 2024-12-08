from flask import Flask, render_template, request, redirect, url_for, flash
from flask_mysqldb import MySQL

app = Flask(__name__)
app.secret_key = 'super_secret_key'

app.config['MYSQL_HOST'] = 'localhost'
app.config['MYSQL_USER'] = 'root'
app.config['MYSQL_PASSWORD'] = ''
app.config['MYSQL_DB'] = 'lotes'

mysql = MySQL(app)

@app.route("/")
def index():
    return render_template("base.html")

@app.route("/categorias")
def categorias():
    cursor = mysql.connection.cursor()
    cursor.execute("SELECT * FROM CategoriasProducto")
    categorias = cursor.fetchall()
    cursor.close()
    return render_template("categorias.html", categorias=categorias)

@app.route('/categorias/add', methods=['POST'])
def add_categoria():
    nombre = request.form['nombre']
    cursor = mysql.connection.cursor()
    cursor.execute("INSERT INTO CategoriasProducto (nombre) VALUES (%s)", [nombre])
    mysql.connection.commit()
    flash('Categoría agregada correctamente.')
    return redirect(url_for('categorias'))

@app.route('/categorias/edit/<int:id>', methods=['POST'])
def edit_categoria(id):
    nombre = request.form['nombre']
    cursor = mysql.connection.cursor()
    cursor.execute("UPDATE CategoriasProducto SET nombre = %s WHERE id = %s", (nombre, id))
    mysql.connection.commit()
    flash('Categoría editada correctamente.')
    return redirect(url_for('categorias'))

@app.route('/categorias/delete/<int:id>', methods=['GET'])
def delete_categoria(id):
    cursor = mysql.connection.cursor()
    cursor.execute("DELETE FROM CategoriasProducto WHERE id = %s", (id,))
    mysql.connection.commit()
    flash('Categoría eliminada correctamente.')
    return redirect(url_for('categorias'))

@app.route("/unidades")
def unidades():
    cursor = mysql.connection.cursor()
    cursor.execute("SELECT * FROM UnidadesMedida")
    unidades = cursor.fetchall()
    cursor.close()
    return render_template("unidades.html", unidades=unidades)

@app.route('/unidades/add', methods=['POST'])
def add_unidad():
    nombre = request.form['nombre']
    cursor = mysql.connection.cursor()
    cursor.execute("INSERT INTO UnidadesMedida (nombre) VALUES (%s)", [nombre])
    mysql.connection.commit()
    flash('Unidad de medida agregada correctamente.')
    return redirect(url_for('unidades'))

@app.route('/unidades/edit/<int:id>', methods=['POST'])
def edit_unidad(id):
    nombre = request.form['nombre']
    cursor = mysql.connection.cursor()
    cursor.execute("UPDATE UnidadesMedida SET nombre = %s WHERE id = %s", (nombre, id))
    mysql.connection.commit()
    flash('Unidad de medida editada correctamente.')
    return redirect(url_for('unidades'))

@app.route('/unidades/delete/<int:id>', methods=['GET'])
def delete_unidad(id):
    cursor = mysql.connection.cursor()
    cursor.execute("DELETE FROM UnidadesMedida WHERE id = %s", (id,))
    mysql.connection.commit()
    flash('Unidad de medida eliminada correctamente.')
    return redirect(url_for('unidades'))

@app.route("/productos")
def productos():
    cursor = mysql.connection.cursor()
    cursor.execute("""
        SELECT p.id, p.nombre, c.nombre, p.costo_produccion, u.nombre, p.valor_unidad_medida
        FROM Productos p 
        JOIN CategoriasProducto c ON p.categoria_id = c.id
        JOIN UnidadesMedida u ON u.id = p.unidad_medida_id
    """)
    productos = cursor.fetchall()

    cursor.execute("SELECT * FROM CategoriasProducto")
    categorias = cursor.fetchall()

    cursor.execute("SELECT * FROM UnidadesMedida")
    unidades = cursor.fetchall()
    cursor.close()
    return render_template("productos.html", productos=productos, categorias=categorias, unidades = unidades)

@app.route('/productos/add', methods=['POST'])
def add_producto():
    nombre = request.form['nombre']
    categoria_id = request.form['categoria_id']
    unidad_medida_id = request.form['unidad_medida_id']
    valor_unidad_medida = request.form['valor_unidad_medida']
    costo_produccion = request.form['costo_produccion']
    cursor = mysql.connection.cursor()
    cursor.execute("INSERT INTO Productos (nombre, categoria_id, unidad_medida_id, valor_unidad_medida, costo_produccion) VALUES (%s, %s, %s, %s, %s)", 
                   (nombre, categoria_id, unidad_medida_id, valor_unidad_medida, costo_produccion))
    mysql.connection.commit()
    flash('Producto agregado correctamente.')
    return redirect(url_for('productos'))

@app.route('/productos/edit/<int:id>', methods=['POST'])
def edit_producto(id):
    nombre = request.form['nombre']
    categoria_id = request.form['categoria_id']
    unidad_medida_id = request.form['unidad_medida_id']
    valor_unidad_medida = request.form['valor_unidad_medida']
    costo_produccion = request.form['costo_produccion']
    cursor = mysql.connection.cursor()
    cursor.execute("UPDATE Productos SET nombre=%s, categoria_id=%s, unidad_medida_id=%s, valor_unidad_medida=%s, costo_produccion=%s WHERE id=%s", 
                   (nombre, categoria_id, unidad_medida_id, valor_unidad_medida, costo_produccion, id))
    mysql.connection.commit()
    flash('Producto editado correctamente.')
    return redirect(url_for('productos'))

@app.route('/productos/delete/<int:id>', methods=['GET'])
def delete_producto(id):
    cursor = mysql.connection.cursor()
    cursor.execute("DELETE FROM Productos WHERE id=%s", (id,))
    mysql.connection.commit()
    flash('Producto eliminado correctamente.')
    return redirect(url_for('productos'))

@app.route("/lotes")
def lotes():
    cursor = mysql.connection.cursor()
    cursor.execute("""
        SELECT l.id, p.nombre, l.cantidad, l.fecha_produccion
        FROM Lotes l
        JOIN Productos p ON l.producto_id = p.id
    """)
    lotes = cursor.fetchall()

    cursor.execute("SELECT * FROM Productos")
    productos = cursor.fetchall()

    cursor.close()
    return render_template("lotes.html", lotes=lotes, productos=productos)

@app.route("/lotes/add", methods=["POST"])
def add_lote():
    producto_id = request.form["producto_id"]
    cantidad = request.form["cantidad"]
    fecha_produccion = request.form["fecha_produccion"]
    cursor = mysql.connection.cursor()
    cursor.execute("""
        INSERT INTO Lotes (producto_id, cantidad, fecha_produccion) 
        VALUES (%s, %s, %s)
    """, (producto_id, cantidad, fecha_produccion))
    mysql.connection.commit()
    flash("Lote agregado exitosamente")
    return redirect(url_for("lotes"))

@app.route('/lotes/edit/<int:id>', methods=['POST'])
def edit_lote(id):
    producto_id = request.form['producto_id']
    cantidad = request.form['cantidad']
    fecha_produccion = request.form['fecha_produccion']
    cursor = mysql.connection.cursor()
    cursor.execute("""
        UPDATE Lotes 
        SET producto_id = %s, cantidad = %s, fecha_produccion = %s 
        WHERE id = %s
    """, (producto_id, cantidad, fecha_produccion, id))
    mysql.connection.commit()
    flash('Lote editado correctamente.')
    return redirect(url_for('lotes'))

@app.route('/lotes/delete/<int:id>', methods=['GET'])
def delete_lote(id):
    cursor = mysql.connection.cursor()
    cursor.execute("DELETE FROM Lotes WHERE id = %s", (id,))
    mysql.connection.commit()
    flash('Lote eliminado correctamente.')
    return redirect(url_for('lotes'))



if __name__ == "__main__":
    app.run(debug=True)
