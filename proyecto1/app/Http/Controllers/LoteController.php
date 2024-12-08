<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\Lote;
use App\Models\Producto;

class LoteController extends Controller
{
    public function index()
    {
        $lotes = Lote::with('producto')->get();
        return view('lotes.index', compact('lotes'));
    }

    public function create()
    {
        $productos = Producto::all();
        return view('lotes.create', compact('productos'));
    }

    public function store(Request $request)
    {
        $request->validate([
            'producto_id' => 'required|exists:productos,id',
            'cantidad' => 'required|integer',
            'fecha_produccion' => 'required|date',
        ]);
        
        Lote::create($request->all());
        return redirect()->route('lotes.index')->with('status', 'Lote creado correctamente');
    }

    public function edit($id)
    {
        $lote = Lote::findOrFail($id);
        $productos = Producto::all();
        return view('lotes.edit', compact('lote', 'productos'));
    }

    public function update(Request $request, $id)
    {
        $request->validate([
            'producto_id' => 'required|exists:productos,id',
            'cantidad' => 'required|integer',
            'fecha_produccion' => 'required|date',
        ]);
        
        $lote = Lote::findOrFail($id);
        $lote->update($request->all());
        return redirect()->route('lotes.index')->with('status', 'Lote actualizado correctamente');
    }

    public function destroy($id)
    {
        $lote = Lote::findOrFail($id);
        $lote->delete();
        return redirect()->route('lotes.index')->with('status', 'Lote eliminado correctamente');
    }
}
