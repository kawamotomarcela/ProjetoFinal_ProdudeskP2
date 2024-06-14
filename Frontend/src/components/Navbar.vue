<template>
    <nav class="navbar navbar-expand-lg bg-dark">
        <div class="container-fluid">
            <a class="navbar-brand text-white" href="#">Produdesk</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <router-link to="/" class="nav-link text-white">Dashboard</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/edit" class="nav-link text-white">Edit</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/filter" class="nav-link text-white">Filter</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/categorys" class="nav-link text-white">Categorys</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/about" class="nav-link text-white">About</router-link>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<style scoped>
.navbar {
    background-color: #000;

    .nav-link, .navbar-brand {
        color: #fff !important; 
    }

    .navbar-toggler-icon {
        filter: invert(1); 
    }
}
@media (min-width: 768px) {
    .nav-item {
        display: none !important;
    }

    .navbar {
        height: 70px !important;
    }

    .navbar-brand {
        display: none !important;
    }
}
</style>