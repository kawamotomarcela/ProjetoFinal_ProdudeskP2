<template>
    <div class="about-page">
        <div class="container">
            <div class="row align-items-center">
                <div class="col-lg-6">
                    <h1 class="about-title">Sobre o Produdesk</h1>
                    <p class="about-description">
                        Bem-vindo ao Produdesk, seu destino definitivo para descobrir e gerenciar uma ampla gama de produtos. Nosso objetivo é fornecer produtos da melhor qualidade com um atendimento ao cliente excepcional. Quer você esteja procurando os gadgets tecnológicos mais recentes ou itens essenciais do dia a dia, o Produdesk está aqui para atender a todas as suas necessidades.
                    </p>
                    <p class="about-description">
                        Nossa missão é oferecer uma experiência de compra incomparável, proporcionando conveniência e satisfação do cliente. No Produdesk, acreditamos em qualidade, variedade e acessibilidade. Nossa equipe é dedicada a selecionar uma variedade diversificada de produtos para atender às suas expectativas.
                    </p>
                </div>
                <div class="col-lg-6 text-center">
                    <img src="../assets/img/okuyaso.jpg" class="img-fluid about-img" alt="Sobre o Produdesk">
                </div>
            </div>
            <div class="row mt-5">
                <div class="col-lg-4">
                    <div class="feature-box">
                        <i class="bx bx-box feature-icon"></i>
                        <h3 class="feature-title">Gestão de Dados</h3>
                        <p class="feature-description">
                            Gerencie os dados dos produtos de forma eficiente com nosso sistema intuitivo de banco de dados, garantindo operações de CRUD perfeitas.
                        </p>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="feature-box">
                        <i class="bx bx-filter feature-icon"></i>
                        <h3 class="feature-title">Filtragem Avançada</h3>
                        <p class="feature-description">
                            Utilize opções de filtragem poderosas para refinar suas pesquisas de produtos e encontrar exatamente o que você precisa.
                        </p>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="feature-box">
                        <i class="bx bx-edit-alt feature-icon"></i>
                        <h3 class="feature-title">Edição de Produtos</h3>
                        <p class="feature-description">
                            Edite os detalhes dos produtos de forma simples, atualize preços, descrições e mais com nossas ferramentas de edição fáceis de usar.
                        </p>
                    </div>
                </div>
            </div>
            <div class="row mt-5">
                <div class="col-lg-12 text-center">
                    <h2 class="partnership-title">Nossa Parceria</h2>
                    <p class="partnership-description">
                        Temos orgulho de fazer parceria com Fabrica de Codigos para trazer a você produtos e serviços ainda mais incríveis.
                        Juntos, pretendemos aprimorar sua experiência de compra.
                    </p>
                    <img src="../assets/img/fabrica_codigos.png" class="img-fluid partner-logo" alt="Logo Fabrica de Codigo">
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
.about-page {
    padding: 40px 20px;
    background-color: #f8f9fa;

    .about-title {
        font-size: 36px;
        font-weight: 700;
        margin-bottom: 20px;
    }

    .about-description {
        font-size: 18px;
        color: #6c757d;
        line-height: 1.6;
        margin-bottom: 15px;
    }

    .about-img {
        border-radius: 10px;
        max-width: 50%;
        height: auto;
    }

    .feature-box {
        text-align: center;
        padding: 20px;
        background-color: #fff;
        border: 1px solid #e9ecef;
        border-radius: 10px;
        margin-bottom: 20px;

        .feature-icon {
            font-size: 36px;
            color: #007bff;
            margin-bottom: 15px;
        }

        .feature-title {
            font-size: 24px;
            font-weight: 600;
            margin-bottom: 10px;
        }

        .feature-description {
            font-size: 16px;
            color: #6c757d;
        }
    }

    .partnership-title {
        font-size: 2rem;
        margin-bottom: 20px;
    }

    .partnership-description {
        font-size: 1.2rem;
        margin-bottom: 20px;
    }

    .partner-logo {
        max-width: 200px;
        height: auto;
    }
}
</style>