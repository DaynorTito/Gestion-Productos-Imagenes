<!-- resources/views/lotes/edit.blade.php -->
<!DOCTYPE html>
<html lang="{{ str_replace('_', '-', app()->getLocale()) }}">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Editar Lote</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <a class="navbar-brand" href="/">Gestión de Productos</a>
        <div class="collapse navbar-collapse">
            <ul class="navbar-nav">
                <li class="nav-item">
                    <a class="nav-link {{ request()->routeIs('categorias.index') ? 'text-white' : '' }}" href="{{ route('categorias.index') }}">Categorías</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link {{ request()->routeIs('unidades.index') ? 'text-white' : '' }}" href="{{ route('unidades.index') }}">Unidades</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link {{ request()->routeIs('productos.index') ? 'text-white' : '' }}" href="{{ route('productos.index') }}">Productos</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link {{ request()->routeIs('lotes.edit') ? 'text-white' : '' }}" href="{{ route('lotes.index') }}">Lotes</a>
                </li>
            </ul>
        </div>
    </nav>
    <div class="container mt-4">
        <h2>Editar Lote</h2>
        <form action="{{ route('lotes.update', $lote->id) }}" method="POST">
            @csrf
            @method('PUT')
            <div class="mb-3">
                <label for="producto_id" class="form-label">Producto</label>
                <select class="form-select" id="producto_id" name="producto_id" required>
                    @foreach($productos as $producto)
                        <option value="{{ $producto->id }}" {{ $lote->producto_id == $producto->id ? 'selected' : '' }}>{{ $producto->nombre }}</option>
                    @endforeach
                </select>
            </div>
            <div class="mb-3">
                <label for="cantidad" class="form-label">Cantidad</label>
                <input type="number" class="form-control" id="cantidad" name="cantidad" value="{{ $lote->cantidad }}" required>
            </div>
            <div class="mb-3">
                <label for="fecha_produccion" class="form-label">Fecha de Expiración</label>
                <input type="date" class="form-control" id="fecha_produccion" name="fecha_produccion" value="{{ $lote->fecha_produccion }}" required>
            </div>
            <button type="submit" class="btn btn-primary">Actualizar</button>
            <a href="{{ route('lotes.index') }}" class="btn btn-secondary">Cancelar</a>
        </form>
    </div>
</body>
</html>
