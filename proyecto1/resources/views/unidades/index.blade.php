<!-- resources/views/unidades/index.blade.php -->
<!DOCTYPE html>
<html lang="{{ str_replace('_', '-', app()->getLocale()) }}">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Unidades</title>
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
                    <a class="nav-link {{ request()->routeIs('lotes.index') ? 'text-white' : '' }}" href="{{ route('lotes.index') }}">Lotes</a>
                </li>
            </ul>
        </div>
    </nav>
    <div class="container mt-4">
        <h2>Unidades</h2>
        <a href="{{ route('unidades.create') }}" class="btn btn-success mb-3">Agregar Nueva Unidad</a>

        @if(session('status'))
            <div class="alert alert-success">{{ session('status') }}</div>
        @endif

        <table class="table table-striped">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Operaciones</th>
                </tr>
            </thead>
            <tbody>
                @foreach($unidades as $unidad)
                <tr>
                    <td>{{ $unidad->id }}</td>
                    <td>{{ $unidad->nombre }}</td>
                    <td>
                        <a href="{{ route('unidades.edit', $unidad->id) }}" class="btn btn-primary btn-sm">Editar</a>
                        <form action="{{ route('unidades.destroy', $unidad->id) }}" method="POST" class="d-inline">
                            @csrf
                            @method('DELETE')
                            <button type="submit" class="btn btn-danger btn-sm">Eliminar</button>
                        </form>
                    </td>
                </tr>
                @endforeach
            </tbody>
        </table>
    </div>
</body>
</html>
