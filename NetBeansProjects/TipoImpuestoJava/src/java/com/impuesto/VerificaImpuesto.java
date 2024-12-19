/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/JSP_Servlet/Servlet.java to edit this template
 */
package com.impuesto;

import java.io.IOException;
import java.io.PrintWriter;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 *
 * @author Daynor
 */
@WebServlet(name = "VerificaImpuesto", urlPatterns = {"/VerificaImpuesto"})
public class VerificaImpuesto extends HttpServlet {

    /**
     * Processes requests for both HTTP <code>GET</code> and <code>POST</code>
     * methods.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
        throws ServletException, IOException {
    response.setContentType("text/html;charset=UTF-8");
    String codigoCatastro = request.getParameter("codCatastro");
    String rol = request.getParameter("rol");
    char primerCaract = codigoCatastro.charAt(0);
    int primerDigito = Character.getNumericValue(primerCaract);
    
    String tipoImpuesto = verificaTipoImpuesto(primerDigito);
    
    try (PrintWriter out = response.getWriter()) {
        out.println("<!DOCTYPE html>");
        out.println("<html>");
        out.println("<head>");
        out.println("<title>Tipo de Impuesto</title>");
        out.println("<link href='https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css' rel='stylesheet'>");
        out.println("<style>");
        out.println("body { background-color: #f8f9fa; }");
        out.println(".container { margin-top: 50px; }");
        out.println(".alert { font-size: 1.2em; }");
        out.println(".large-text { font-size: 1.5em; }"); // Aumentar tamaño del texto
        out.println("</style>");
        out.println("</head>");
        out.println("<body>");
        out.println("<div class='container'>");
        out.println("<h1>Desde Java</h1>");
        out.println("<div class='alert alert-info'>"); // Usar Bootstrap para estilos
        out.println("El tipo de impuesto para cod catastral <span class='large-text'>" + 
                        codigoCatastro + 
                        "</span> es: <span class='large-text'>" + 
                        tipoImpuesto + 
                        "</span>.");
        out.println("</div>");
        out.println("<a href='http://localhost:800/primerExam/dos/catastro/index.php?rol=" + rol + "' class='btn btn-primary'>Volver</a>");
        out.println("</div>");
        out.println("</body>");
        out.println("</html>");
    }
}

    
    private String verificaTipoImpuesto(int primerDigitoCod) {
        return switch (primerDigitoCod) {
            case 1 -> "Alto";
            case 2 -> "Medio";
            case 3 -> "Bajo";
            default -> "N/A";
        };
    }

    // <editor-fold defaultstate="collapsed" desc="HttpServlet methods. Click on the + sign on the left to edit the code.">
    /**
     * Handles the HTTP <code>GET</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Handles the HTTP <code>POST</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Returns a short description of the servlet.
     *
     * @return a String containing servlet description
     */
    @Override
    public String getServletInfo() {
        return "Short description";
    }// </editor-fold>


}
