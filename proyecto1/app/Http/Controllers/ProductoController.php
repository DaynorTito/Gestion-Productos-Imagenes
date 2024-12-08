<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\Producto;
use App\Models\Categoria;
use App\Models\Unidad;

class ProductoController extends Controller
{
    public function index()
    {
        $productos = Producto::with('categoria', 'unidad_medida')->get();
        return view('productos.index', compact('productos'));
    }

    public function create()
    {
        $categorias = Categoria::all();
        $unidades = Unidad::all();
        return view('productos.create', compact('categorias', 'unidades'));
    }

    public function store(Request $request)
    {
        Producto::create($request->all());
        return redirect()->route('productos.index')->with('status', 'Producto creado correctamente');
    }
    

    public function edit($id)
    {
        $producto = Producto::findOrFail($id);
        $categorias = Categoria::all();
        $unidades = Unidad::all();
        return view('productos.edit', compact('producto', 'categorias', 'unidades'));
    }

    public function update(Request $request, $id)
    {
        $producto = Producto::findOrFail($id);
        $producto->update($request->only(['nombre', 'categoria_id', 'unidad_medida_id', 'costo_produccion', 'valor_unidad_medida']));
        return redirect()->route('productos.index')->with('status', 'Producto actualizado correctamente');
    }

    public function destroy($id)
    {
        $producto = Producto::findOrFail($id);
        $producto->delete();
        return redirect()->route('productos.index')->with('status', 'Producto eliminado correctamente');
    }
}
