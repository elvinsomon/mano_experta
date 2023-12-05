import 'package:flutter/material.dart';

import '../data/models/professional.dart';

class ProfessionalDetailPage extends StatefulWidget {
  final Professional professional;

  ProfessionalDetailPage({required this.professional});

  @override
  _ProfessionalDetailPageState createState() => _ProfessionalDetailPageState();
}

class _ProfessionalDetailPageState extends State<ProfessionalDetailPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Detalles del Profesional'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            _buildDetailRow('Usuario', widget.professional.userName ?? 'No user'),
            _buildDetailRow('Nombre', widget.professional.fullName ?? 'No name'),
            _buildDetailRow('Teléfonos', _getPhoneNumbers(widget.professional.phoneNumbers)),
            _buildDetailRow('Emails', _getEmails(widget.professional.emails)),
            _buildDetailRow('Categoría', _getCategories(widget.professional.category)),
            _buildDetailRow('Horario de trabajo', _getWorkingHours(widget.professional.workingHours)),
            // Puedes agregar más detalles según sea necesario
          ],
        ),
      ),
    );
  }

  Widget _buildDetailRow(String label, String value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            label,
            style: TextStyle(
              fontSize: 18,
              fontWeight: FontWeight.bold,
              color: Colors.blue,
            ),
          ),
          SizedBox(height: 8),
          Text(
            value,
            style: TextStyle(
              fontSize: 16,
            ),
          ),
        ],
      ),
    );
  }

  String _getPhoneNumbers(List<PhoneNumber>? phoneNumbers) {
    return phoneNumbers?.map((phoneNumber) => phoneNumber.number).join(", ") ?? 'No phone numbers';
  }

  String _getEmails(List<Email>? emails) {
    return emails?.map((email) => email.address).join(", ") ?? 'No emails';
  }

  String _getCategories(List<Category>? categories) {
    return categories?.map((category) => category.name).join(", ") ?? 'No categories';
  }

  String _getWorkingHours(List<WorkingHour>? workingHours) {
    return workingHours?.map((workingHour) =>
        '${workingHour.dayName}, de ${workingHour.start} a ${workingHour.end}')
        .join("\n") ?? 'No working hours';
  }
}
