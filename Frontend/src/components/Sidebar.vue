<template>
    <div class="sidebar position-fixed d-flex flex-column flex-shrink-0 p-3 text-dark bg-dark h-100"
        style="width: 280px;">
        <a href="/" class="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-dark text-decoration-none">
            <svg class="bi me-2" width="40" height="32">
                <use xlink:href="#bootstrap"></use>
            </svg>
            <span class="fs-4 text-white">Produdesk</span>
        </a>
        <hr>
        <ul class="nav nav-pills flex-column mb-auto">
            <li>
                <router-link to="/" class="nav-link text-white d-flex align-items-center">
                    <box-icon name='home' color="#ffffff" class="me-2"></box-icon>
                    Dashboard
                </router-link>
            </li>
            <li>
                <router-link to="/edit" class="nav-link text-white d-flex align-items-center">
                    <box-icon name='grid'color="#ffffff"  class="me-2"></box-icon>
                    Edit
                </router-link>
            </li>
            <router-link to="/categorys" class="nav-link text-white d-flex align-items-center">
                    <box-icon name='box' color="#ffffff"  type='solid' class="me-2"></box-icon>
                    Categorys
                </router-link>
            <li>
                <router-link to="/filter" class="nav-link text-white d-flex align-items-center">
                    <box-icon name='filter-alt'color="#ffffff"  type='solid' class="me-2" ></box-icon>
                    Filter
                </router-link>
            </li>
            <li>
                <router-link to="/about" class="nav-link text-white d-flex align-items-center">
                    <box-icon name='box' color="#ffffff"  type='solid' class="me-2"></box-icon>
                    About
                </router-link>
            </li>
          
        </ul>
        <hr>
        <div class="dropdown">
            <a href="#" class="d-flex align-items-center text-dark text-decoration-none dropdown-toggle"
                id="dropdownUser1" data-bs-toggle="dropdown" aria-expanded="false">
                <img src="../assets/img/okuyaso.jpg" alt="" width="32" height="32" class="rounded-circle me-2">
                <strong>Admin</strong>
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="dropdownUser1">
                <li><a class="dropdown-item" href="#">New project...</a></li>
                <li><a class="dropdown-item" href="#">Settings</a></li>
                <li><a class="dropdown-item" href="#">Profile</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="#">Sign out</a></li>
            </ul>
        </div>
    </div>
</template>

<style scoped>

.box-icon {
    color: white !important; 
}

.dropdown-toggle {
    color: white !important; 
}

@media (max-width: 768px) {
    .sidebar {
        display: none !important;
    }
}
</style>