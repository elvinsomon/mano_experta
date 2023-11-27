import '../models/professional.dart';
import '../provider/professional_api.dart';

class ProfessionalService {
  final _api = ProfessionalApi();
  Future<List<Professional>?> getProfessionalsByCaregory(String? categoryCode) async {
    return _api.getGetProfessionalByCategory(categoryCode);
  }
}