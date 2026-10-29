# Plan de implementación PetCare

## Secuencia de PR
1. Módulo 0 — Especificación y plan.
2. Módulo 1 — Base técnica y CI.
3. Módulo 2 — Sistema visual.
4. Módulo 3 — Identidad y roles.
5. Módulo 4 — Catálogo y mascotas.
6. Módulo 5 — Agenda y citas.
7. Módulo 6 — Pagos sandbox.
8. Módulo 7 — Servicios externos.
9. Módulo 8 — Sugerencias con IA.
10. Módulo 9 — Despliegue y entrega.

## Tecnología
ASP.NET Core Web API con .NET 10, EF Core, Npgsql, ASP.NET Core Identity y PostgreSQL. Frontend con React, Vite, JavaScript, React Router y Motion. Pruebas con xUnit, PostgreSQL, Vitest y Playwright. Publicación con Docker multietapa y origen único.

## Reglas clave
Las citas usan la zona horaria de la clínica para seleccionar horario y UTC para almacenar instantes. Duran 15, 30 o 60 minutos; se agendan de 2 horas a 60 días. El servidor recalcula disponibilidad y precio, asigna el horario en transacción e implementa idempotencia. Stripe solo en sandbox; la cita se confirma tras validar firma, evento, importe, moneda y estado del proveedor. Tareas de correo, expiración y reembolso son durables. Secretos solo en user-secrets o variables de entorno.

## Historias por módulo
Módulo 2: HU01. Módulo 3: HU02. Módulo 4: HU01, HU03 y HU07. Módulo 5: HU04–HU06. Módulo 6: pagos de HU05–HU06. Módulo 7: HU08–HU09. Módulo 8: HU10. Módulo 9: evidencia final.

La sección 8–11 del documento fuente se completará únicamente con resultados, enlaces, métricas y datos reales del proyecto. Cada módulo tendrá rama y PR; se ejecutarán las pruebas del módulo y se esperará aprobación antes de avanzar.
