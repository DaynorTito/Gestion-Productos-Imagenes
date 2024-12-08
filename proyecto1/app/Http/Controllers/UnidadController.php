<?php

namespace App\Http\Controllers;

use App\Models\Unidad;
use Illuminate\Http\Request;

class UnidadController extends Controller
{
    public function index()
    {
        $unidades = Unidad::all();
        return view('unidades.index', compact('unidades'));
    }

    public function create()
    {
        return view('unidades.create');
    }

    public function store(Request $request)
    {
        $request->validate([
            'nombre' => 'required|string|max:255',
        ]);
        
        Unidad::create($request->all());
        return redirect()->route('unidades.index')->with('status', 'Unidad creada correctamente');
    }

    public function edit($id)
    {
        $unidad = Unidad::findOrFail($id);
        return view('unidades.edit', compact('unidad'));
    }

    public function update(Request $request, $id)
    {
        $request->validate([
            'nombre' => 'required|string|max:255',
        ]);
        
        $unidad = Unidad::findOrFail($id);
        $unidad->update($request->all());
        return redirect()->route('unidades.index')->with('status', 'Unidad actualizada correctamente');
    }

    public function destroy($id)
    {
        $unidad = Unidad::findOrFail($id);
        $unidad->delete();
        return redirect()->route('unidades.index')->with('status', 'Unidad eliminada correctamente');
    }
}
