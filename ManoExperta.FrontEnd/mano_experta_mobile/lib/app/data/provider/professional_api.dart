import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/professional.dart';

class ProfessionalApi {
  Future<List<Professional>?> getGetProfessionalByCategory(categoryCode) async {
    var client = http.Client();
    var uri = Uri.parse('http://localhost:5198/Professional/GetByCategory?categoryCode=${categoryCode}');
    var response = await client.get(uri);
    if (response.statusCode == 200) {
      return professionalFromJson(const Utf8Decoder().convert(response.bodyBytes));
    }
    return null;
  }
}